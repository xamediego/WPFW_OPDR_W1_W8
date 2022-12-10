namespace ConsoleApp4;

public class Login
{
    public static string GetHtml()
    {
        return 
        @"<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <title>Login</title>
    <style>
        p {
            color: #383838;
            font-size: 14px;
            font-family: sans-serif;
        }
        .RootDiv {
            all: unset;
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center
        }
        .FormDiv {
            all: unset;
            display: flex;
            align-items: center;
            justify-content: center;
            border-radius: 4px;
            height: 600px;
            width: 400px;
        }
        .InputForm {
            all: unset;
            border: 1px solid #252525;
            border-radius: 4px;
            width: 350px;
            height: 37px;
            padding-left: 8px;
            font-size: 18px;
            font-family: sans-serif;
            cursor: text;
        }
        .LoginButton {
            all: unset;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            background: #3b70c8;
            border-radius: 5px;
            color: whitesmoke;
            font-size: 20px;
            font-family: sans-serif;
            width: 350px;
            height: 40px;
            margin-top: 40px;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
            cursor: pointer;
        }
        .LoginButton:hover {
            opacity: 0.8;
        }
    </style>
    <base href='.'>
    <script>
        const login = () => {
            console.log('Requested login');
            const req = {
                username: document.getElementById('username').value,
                password: document.getElementById('password').value
            };
            fetch('http://localhost:8080/api/auth/login', {
                headers: {
                    'Content-Type': 'application/json'
                },
                method: 'POST',
                body: JSON.stringify(req),
            }).then(response => {
                if (response.status === 200) return response.text();
                else if (response.status === 401 || response.status === 403) console.log('Invalid credentials');
                else console.log('Server Error');
            }).then((data) => {
                if (data) {
                    console.log('Setting jwt');
                    console.log(data)
                    let base64Url = data.split('.')[1];
                    let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
                    let jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
                        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
                    }).join(''));
                    console.log(JSON.parse(jsonPayload));
                    let payload = JSON.parse(jsonPayload);
                    console.log
                    console.log(payload.exp);
                    document.cookie = 'JWT' + '=' + data; expires=JSON.parse(jsonPayload).exp;
                    window.location.href = 'http://localhost:4040/main';
                }
            });
        }
    </script>
</head>
<body>
<div class='RootDiv'>
    <div class='FormDiv'>
        <div onsubmit='login()'>
            <div class='UsernameDiv'>
                <div>
                    <p>Email</p>
                </div>
                <div>
                    <label>
                        <input id='username' class='InputForm' placeholder='Email Adress' type='text'>
                    </label>
                </div>
            </div>
            <div class='PasswordDiv'>
                <div>
                    <p>Password</p>
                </div>
                <div>
                    <label>
                        <input id='password' class='InputForm' placeholder='Password' type='password'>
                    </label>
                </div>
            </div>
            <div>
                <button onclick='login()' class='LoginButton'>Login</button>
            </div>
        </div>
    </div>
</div>
</body>
</html>";
    }
}