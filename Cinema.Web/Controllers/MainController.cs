using Cinema.Data.Features.Movies.Queries.GetMoviesWithEntries;
using Cinema.Data.Features.Movies.Queries.GetMovieWithEntries;
using Cinema.Data.Features.Prices.Queries.GetPricesForList;
using Cinema.Data.Features.Purchases.Commands.CreatePurchaseTickets;
using Cinema.Data.Features.Purchases.Queries.GetPurchaseByKey;
using Cinema.Data.Features.SeatPrices.Queries.GetSeatPrice;
using Cinema.Data.Features.SeatPrices.Queries.GetSeatPricesForListFilt;
using Cinema.Data.Features.Seats.Queries.GetSeats;
using Cinema.Data.Features.Tickets.Commands.CancelTickets;
using Cinema.Data.Services.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
using ZXing.QrCode;

namespace Cinema.Web.Controllers
{
    [Route("api/main")]
    public sealed class MainController : Controller
    {
        private readonly EmailService _emailService;
        public MainController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        [Route("movies")]
        public async Task<IActionResult> GetMoviesWithEntriesAsync(
            [FromQuery] CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetMoviesWithEntriesQuery(), cancellationToken));
        }

        [HttpGet]
        [Route("movie/{movieId:int}")]
        public async Task<IActionResult> GetMovieWithEntriesAsync(
            [FromRoute] int movieId,
            [FromQuery] CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetMovieWithEntriesQuery(movieId), cancellationToken));
        }

        [HttpGet]
        [Route("pricesforlist/{priceListId:int}")]
        public async Task<IActionResult> GetPricesForListAsync(
            [FromRoute] int priceListId,
            [FromQuery] CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetPricesForListQuery(priceListId), cancellationToken));
        }
        [HttpGet]
        [Route("seats")]
        public async Task<IActionResult> GetSeatsAsync(
            [FromQuery] CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetSeatsQuery(), cancellationToken));
        }

        [HttpGet("seatprices/{tableEntryId:int}")]
        public async Task<IActionResult> GetSeatPricesFilteredAsync(
            [FromRoute] int tableEntryId,
            [FromQuery] CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetSeatPricesForListFiltQuery(tableEntryId), cancellationToken));
        }

        [HttpGet]
        [Route("purchase/{RefundKey}")]
        public async Task<IActionResult> GetTicketsByPurchaseAsync(
            [FromRoute] string refundKey,
            [FromQuery] CancellationToken cancellationToken)
        {
            var purchase = await Mediator.Send(new GetPurchaseByKeyQuery(refundKey), cancellationToken);
            return Ok(purchase);
            //return Ok(await Mediator.Send(new GetTicketsByPurchaseQuery(purchase.Id), cancellationToken));
        }

        [HttpPost]
        [Route("tickets")]
        public async Task<IActionResult> CreateTicketAsync(
            [FromBody] CreatePurchaseTicketsDto dto,
            [FromQuery] CancellationToken cancellationToken)
        {
            var purchaseKey = await Mediator.Send(new CreatePurchaseTicketsCommand(dto), cancellationToken);
            var purchase = await Mediator.Send(new GetPurchaseByKeyQuery(purchaseKey), cancellationToken);
            var qr = new QR();
            var ticketKeys = new List<string>();

            string str = "";
            int c = 1;
            foreach (var ticket in purchase.Tickets)
            {
                str += $"{c} - в {ticket.Seat.Row} ряду {ticket.Seat.Column} место. Стоимость {ticket.Price} руб.<br>";
                c++;
            }

            string ticketsHtml = "";
            foreach (var ticket in purchase.Tickets)
            {
                var byteArray = qr.Generate(ticket.Key);
                var base64Image = Convert.ToBase64String(byteArray);
                ticketsHtml += $"<div class='ticket'>" +
                    $"<div class='text'>{purchase.TableEntry.Movie.Title}, {purchase.TableEntry.DateTime}<br>" +
                    $"{ticket.Seat.Column} место, {ticket.Seat.Row} ряд, <br> стоимость {ticket.Price} руб. <br> Дата покупки: {purchase.DateTime} </div>" +
                    $"<img src='data:image/png;base64,{base64Image}'alt='Red dot'/></div>";
            }
            var htmlString =
                "<html><head>" +
                "<style>" +
                "img {height: 200px; widht: 200px; margin: 30px}" +
                ".text {width: 70%; margin: 30px;}" +
                ".ticket {display: flex; background-color: #dbdbdb; width: 90%; font-size: 30px; border-style: solid}" +
                "</style></head>" +
                $"<body>{ticketsHtml}" +
                "</body></html>";

            var converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Options.MarginLeft = 43;
            converter.Options.MarginTop = 43;
            converter.Options.MarginRight = 43;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            PdfDocument doc = converter.ConvertHtmlString(htmlString);
            byte[] pdfBytes = doc.Save();
            doc.Close();

            await _emailService.SendWithPdfAsync(
                dto.EmailAddress, //to
                $"Вы приобрели билеты в кинотеатре Помор.", //subject
                $"Вами были приобретены билеты на фильм {purchase.TableEntry.Movie.Title} на {purchase.TableEntry.DateTime:f}:<br>" +
                str +
                $"Вы можете вернуть билеты, перейдя по ссылке: http://192.168.100.5:8080/purchase/{purchaseKey}.<br>" +
                "Перед посещением сеанса необходимо предьявить контролеру билеты из прикрепленного файла.", //Html документ
                pdfBytes, //pdf file
                cancellationToken);

            return NoContent();
        }

        [HttpPut]
        [Route("purchase")]
        public async Task<IActionResult> CancelTickets(
            [FromBody] CancelTicketsDto Dto,
            [FromQuery] CancellationToken cancellationToken)
        {
            await Mediator.Send(
                new CancelTicketsCommand(Dto),
                cancellationToken);

            return NoContent();
        }
        [Authorize(Roles = "admin,cassir,controler")]
        [HttpGet("pdf/{purchaseKey}")]
        public async Task<IActionResult> GetPdfFile(
            [FromRoute] string purchaseKey,
            [FromQuery] CancellationToken cancellationToken)
        {
            var purchase = await Mediator.Send(new GetPurchaseByKeyQuery(purchaseKey), cancellationToken);
            var qr = new QR();

            string ticketsHtml = "";
            foreach (var ticket in purchase.Tickets)
            {
                var byteArray = qr.Generate(ticket.Key);
                var base64Image = Convert.ToBase64String(byteArray);
                ticketsHtml += $"<div class='ticket'>" +
                    $"<div class='text'>{purchase.TableEntry.Movie.Title}, {purchase.TableEntry.DateTime}<br>" +
                    $"{ticket.Seat.Column} место, {ticket.Seat.Row} ряд, <br> стоимость {ticket.Price} руб. <br> Дата покупки: {purchase.DateTime} </div>" +
                    $"<img src='data:image/png;base64,{base64Image}'alt='Red dot'/></div>";
            }
            var htmlString =
                "<html><head>" +
                "<style>" +
                "img {height: 200px; widht: 200px; margin: 30px}" +
                ".text {width: 70%; margin: 30px;}" +
                ".ticket {display: flex; background-color: #dbdbdb; width: 90%; font-size: 30px; border-style: solid}" +
                "</style></head>" +
                $"<body>{ticketsHtml}" +
                "</body></html>";

            var converter = new HtmlToPdf();
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.CssMediaType = HtmlToPdfCssMediaType.Print;
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
            converter.Options.MarginLeft = 43;
            converter.Options.MarginTop = 43;
            converter.Options.MarginRight = 43;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            PdfDocument doc = converter.ConvertHtmlString(htmlString);
            byte[] pdfBytes = doc.Save();
            doc.Close();

            return File(pdfBytes, "application/pdf");
        }
        public class QR
        {
            public byte[] Generate(string text)
            {
                Byte[] byteArray;
                var width = 250;
                var height = 250;
                var margin = 0;
                var qrCodeWriter = new ZXing.BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new QrCodeEncodingOptions
                    {
                        Height = height,
                        Width = width,
                        Margin = margin
                    }
                };
                var pixelData = qrCodeWriter.Write(text);

                using (var bitmap = new System.Drawing.Bitmap(pixelData.Width, pixelData.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb))
                {
                    using (var ms = new MemoryStream())
                    {
                        var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, pixelData.Width, pixelData.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                        try
                        {
                            System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, bitmapData.Scan0, pixelData.Pixels.Length);
                        }
                        finally
                        {
                            bitmap.UnlockBits(bitmapData);
                        }
                        bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        byteArray = ms.ToArray();
                    }
                }
                return byteArray;
            }
        }
    }
}
