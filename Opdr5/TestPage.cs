namespace ConsoleApp4;


public class TestPage
{
    public static string GetHtml()
    {
        return @"<!DOCTYPE html>
            <html lang='en'>
            <head>
            <meta charset='UTF-8'>
            <title>Title</title>
            <style>
        
        .RootDiv {
            all: unset;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center
        }
            </style>
            </head>
            <body>
            <div class='RootDiv'>
            <h1>
            Logged In!
            </h1>
            </div>
            </body>
            </html >";
    }
}