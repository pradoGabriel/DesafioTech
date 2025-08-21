import React from 'react';
import { BrowserRouter as Router, Route, Routes, Navigate } from 'react-router-dom';
import LoginPage from './pages/LoginPage.tsx';
import FuncionarioListPage from './pages/FuncionarioListPage.tsx';
import FuncionarioFormPage from './pages/FuncionarioFormPage.tsx';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/login" element={<LoginPage />} />
        <Route path="/funcionario" element={<FuncionarioListPage />} />
        <Route path="/funcionario/new" element={<FuncionarioFormPage />} />
        <Route path="/funcionario/:id/edit" element={<FuncionarioFormPage />} />
        <Route path="*" element={<Navigate to="/login" />} />
      </Routes>
    </Router>
  );
}

export default App;
