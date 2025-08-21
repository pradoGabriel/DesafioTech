import React, { useEffect, useState } from 'react';
import { getEmployees, deleteEmployee } from '../services/funcionarioService.ts';
import { useNavigate } from 'react-router-dom';
import '../styles/FuncionarioListPage.css';

const FuncionarioListPage: React.FC = () => {
  const [employees, setEmployees] = useState<any[]>([]);
  const [error, setError] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
  getEmployees()
      .then(setEmployees)
      .catch(() => setError('Erro ao carregar funcionários'));
  }, []);

  const handleDelete = async (id: number) => {
    if (window.confirm('Confirma exclusão?')) {
      await deleteEmployee(id);
      setEmployees(employees.filter(e => e.id !== id));
    }
  };

  return (
    <div className="list-bg">
      <div className="list-container">
        <h2 className="list-title">Funcionários</h2>
        <button onClick={() => navigate('/funcionario/new')} className="list-btn">Novo Funcionário</button>
        {error && <p className="list-error">{error}</p>}
        <table className="list-table">
          <thead className="list-thead">
            <tr>
              <th className="list-th">Nome</th>
              <th className="list-th">Email</th>
              <th className="list-th">Documento</th>
              <th className="list-th">Telefones</th>
              <th className="list-th">Nome do Gerente</th>
              <th className="list-th">Ações</th>
            </tr>
          </thead>
          <tbody>
            {employees.map(emp => (
              <tr key={emp.id} className="list-tr">
                <td className="list-td">{emp.nome} {emp.sobrenome}</td>
                <td className="list-td">{emp.email}</td>
                <td className="list-td">{emp.documento}</td>
                <td className="list-td">{Array.isArray(emp.telefones) ? emp.telefones.map(t => t.numero).join(', ') : ''}</td>
                <td className="list-td">{emp.nomeGerente}</td>
                <td className="list-td">
                  <button onClick={() => navigate(`/funcionario/${emp.id}/edit`)} className="list-action-btn">Editar</button>
                  <button onClick={() => handleDelete(emp.id)} className="list-delete-btn">Excluir</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default FuncionarioListPage;
