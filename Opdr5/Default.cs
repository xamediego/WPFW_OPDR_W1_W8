namespace ConsoleApp4;

public class Default
{
    private static string GetBrowserFromUserAgent(string useragent)
    {
        string[] b = useragent.Split(" ");
        int index = b.Length - 1;
        
        useragent = b[index];
        b = useragent.Split("/");
        return b[0];
    }
    
    public static string GetHtml(string browser)
    {
        return
            @"<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Pretpark Den Haag</title>
    <STYLE>
        .RootDiv {
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            height: 100vh;
        }
        .TopDiv{
            display: flex;
            flex-direction: row;

            justify-content: center;
            align-items: center;

            height: 10vh;
        }
        .TitleDiv {
            display: flex;
            flex-direction: row;

            justify-content: center;
            width: 50vw;
            
            border: 1px black;
        }
        .LoginDiv{
            display: flex;
            flex-direction: row;
            column-gap: 25px;

            justify-content: center;
            width: 50vw;
        }
        .LoginButton {
            all: unset;
            display:flex;
            flex-direction:column;
            align-items: center;
            justify-content: center;
            background: #3b70c8;
            border-radius: 5px;
            color: whitesmoke;
            font-size: 20px;
            font-family: sans-serif;
            width: 120px;
            height: 35px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
            cursor: pointer;
        }
        .LoginButton:hover {
            opacity: 0.8;
        }
        .SignUpButton{
            all: unset;
            display:flex;
            flex-direction:column;
            align-items: center;
            justify-content: center;
            background: #3b70c8;
            border-radius: 5px;
            color: whitesmoke;
            font-size: 20px;
            font-family: sans-serif;
            width: 120px;
            height: 35px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
            cursor: pointer;
        }
        .SignUpButton:hover {
            opacity: 0.8;
        }
        .ContentDiv {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            
            height: 80vh;
            width: 100vw;
        }
        .LowDiv {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            
            height: 10vh;
            width: 100vw;
        }
    </STYLE>

    <script>
        const GetLogin = () => {
            window.location.href = 'http://localhost:4040/login';
        }
    </script>

</head>
<body>
<div class='RootDiv'>
    
    <div class='TopDiv'>
        <div class='TitleDiv'>
            <p>
                <a href='https://nl.wikipedia.org/wiki/Den_Haag'>
                    Welkom bij de website van Pretpark Den Haag!
                </a>
            </p>
        </div>
        <div class='LoginDiv'>
            <button class='LoginButton' onclick='GetLogin()'>
                Login
            </button>
            <button class='SignUpButton'>
                Sign up
            </button>
        </div>
    </div>
    
    <div class='ContentDiv'>" +
            $"<p id='greeting'>{GetBrowserFromUserAgent(browser)}</p>" +
            @"</div>
    
    <div class='LowDiv'>
        <p>
            <a href='http://localhost:4040/contact'>
                Contact
            </a>
        </p>
    </div>
    
</div>
</body>
</html>";
    }
}