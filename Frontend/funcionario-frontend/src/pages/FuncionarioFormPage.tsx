import React, { useState, useEffect } from 'react';
import { createEmployee, getEmployee, updateEmployee } from '../services/funcionarioService.ts';
import { useNavigate, useParams } from 'react-router-dom';
import '../styles/FuncionarioFormPage.css';

const FuncionarioFormPage: React.FC = () => {
  const [funcionario, setFuncionario] = useState<any>({ telefones: [{ numero: '' }, { numero: '' }] });
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const { id } = useParams();

  useEffect(() => {
    if (id) {
      getEmployee(Number(id)).then(setFuncionario).catch(() => setError('Erro ao carregar funcionário'));
    }
  }, [id]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    setFuncionario({ ...funcionario, [e.target.name]: e.target.value });
  };

  const handlePhoneChange = (idx: number, value: string) => {
    const telefones = [...funcionario.telefones];
    telefones[idx] = { ...telefones[idx], numero: value };
    setFuncionario({ ...funcionario, telefones });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      if (id) {
        funcionario.id = Number(id);
        await updateEmployee(Number(id), funcionario);
      } else {
        funcionario.id = 0;
        await createEmployee(funcionario);
      }
      navigate('/funcionario');
    } catch (err: any) {
      setError(err.response?.data || 'Erro ao salvar');
    }
  };

  return (
    <div className="form-bg">
      <div className="form-container">
        <h2 className="form-title">{id ? 'Editar' : 'Novo'} Funcionário</h2>
        <form onSubmit={handleSubmit} className="form-main">
          <input name="nome" placeholder="Nome" value={funcionario.nome || ''} onChange={handleChange} required className="form-input" />
          <input name="sobrenome" placeholder="Sobrenome" value={funcionario.sobrenome || ''} onChange={handleChange} required className="form-input" />
          <input name="email" placeholder="E-mail" value={funcionario.email || ''} onChange={handleChange} required className="form-input" />
          <input name="documento" placeholder="Documento" value={funcionario.documento || ''} onChange={handleChange} required className="form-input" />
          <input name="dataNascimento" type="date" value={funcionario.dataNascimento || ''} onChange={handleChange} required className="form-input" />
          <input name="nomeGerente" placeholder="Nome do Gerente" value={funcionario.nomeGerente || ''} onChange={handleChange} className="form-input" />
          <input name="passwordHash" type="password" placeholder="Senha" value={funcionario.passwordHash || ''} onChange={handleChange} required={!id} className="form-input" />
          <select name="role" value={funcionario.role || 'Funcionario'} onChange={handleChange} required className="form-select">
            <option value="Funcionario">Funcionario</option>
            <option value="Lider">Lider</option>
            <option value="Diretor">Diretor</option>
          </select>
          <div>
            <label className="form-label">Telefones:</label>
            {[0,1].map(idx => (
              <input key={idx} placeholder={`Telefone ${idx+1}`} value={funcionario.telefones[idx]?.numero || ''} onChange={e => handlePhoneChange(idx, e.target.value)} required className="form-phone-input" />
            ))}
          </div>
          <button type="submit" className="form-btn">Salvar</button>
        </form>
        {error && <p className="form-error">{error}</p>}
      </div>
    </div>
  );
};

export default FuncionarioFormPage;
