import React from 'react';
import './App.css';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import LoginForm from './Components/LoginForm';
import RegistrationForm from './Components/RegistrationForm';
import Dashboard from './Components/Dashboard';
import ForgotPassword from './Components/Forgot-password';

function App() {
    return (
        <Router>
            <div className="App">
                <header className="App-header">
                    <h1 className="App-header">
                        <Routes>
                            <Route path="/" element={<LoginForm />} />
                            <Route path="/registration" element={<RegistrationForm />} />
                            <Route path="/dashboard" element={<Dashboard />} />
                            <Route path="/Forgot-password" element={<ForgotPassword />} />
                        </Routes>
                    </h1>
                </header>
            </div>
        </Router>
    );
}

export default App;