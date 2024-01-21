import React, { Component } from 'react';

class Login extends Component {
    constructor(props) {
        super(props);

        this.state = {
            username: '',
            password: ''
        };
    }

    handleInputChange = (e) => {
        this.setState({
            [e.target.id]: e.target.value
        });
    };

    handleLogin = async () => {
        const { username, password } = this.state;

        try {
            var myHeaders = new Headers();
            myHeaders.append("Content-Type", "application/json");

            var raw = JSON.stringify({
                "UserName": username,
                "Password": password,
                token: null
            });

            var requestOptions = {
                method: 'POST',
                headers: myHeaders,
                body: raw,
                redirect: 'follow'
            };

            const response = await fetch("/api/Authentication/Login", requestOptions);
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }

            if (response.ok) {
                var token = await response.text();
                console.log(token);
                localStorage.setItem('token', token);

                // Update the state to store the token
                this.setState({
                    token: token
                });
            }

        } catch (error) {
            console.error('Login failed:', error.message);
        }
    };

    handleLogout = () => {
        // Remove the token from local storage
        localStorage.removeItem('token');
        // Optional: You can also reset the state if needed
        this.setState({
            username: '',
            password: ''
        });
    };

    render() {
        const token = localStorage.getItem('token');

        return (
            <div>
                <h2>Login</h2>
                <div className="form-group">
                    <label htmlFor="username">Username:</label>
                    <input
                        type="text"
                        id="username"
                        placeholder="Enter admin or guest"
                        value={this.state.username}
                        onChange={this.handleInputChange}
                    />
                </div>

                <div className="form-group">
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        placeholder="Password is 1234"
                        value={this.state.password}
                        onChange={this.handleInputChange}
                    />
                </div>

                <button type="button" onClick={this.handleLogin}>
                    Login
                </button>

                {/* Display the token if it's not null */}
                {token !== null && (
                    <div>
                        <label style={{ fontSize: '15px', width: '50%', display: 'block', wordBreak: 'break-all' }}>Token: {token}</label>
                        <button type="button" onClick={this.handleLogout}>
                            Logout
                        </button>
                    </div>
                )}
            </div>
        );
    }
}

export default Login;
