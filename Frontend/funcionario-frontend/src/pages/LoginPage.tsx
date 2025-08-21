import React, { useState } from 'react';
import axios from 'axios';
import '../styles/LoginPage.css';

const LoginPage: React.FC = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const res = await axios.post(`${process.env.REACT_APP_API_URL}/auth/login`, { email, password });
      localStorage.setItem('token', res.data.token);
      window.location.href = '/funcionario';
    } catch (err: any) {
      setError(err.response?.data || 'Erro ao autenticar');
    }
  };

  return (
    <div className="login-bg">
      <div className="login-container">
        <h2 className="login-title">Login</h2>
        <form onSubmit={handleLogin} className="login-form">
          <input type="email" placeholder="E-mail" value={email} onChange={e => setEmail(e.target.value)} required className="login-input" />
          <input type="password" placeholder="Senha" value={password} onChange={e => setPassword(e.target.value)} required className="login-input" />
          <button type="submit" className="login-btn">Entrar</button>
        </form>
        {error && <p className="login-error">{error}</p>}
      </div>
    </div>
  );
};

export default LoginPage;
