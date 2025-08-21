import axios from 'axios';

const API_URL = process.env.REACT_APP_API_URL;

export const getEmployees = async () => {
  const token = localStorage.getItem('token');
  const res = await axios.get(`${API_URL}/funcionario`, {
    headers: { Authorization: `Bearer ${token}` }
  });
  return res.data;
};

export const getEmployee = async (id: number) => {
  const token = localStorage.getItem('token');
  const res = await axios.get(`${API_URL}/funcionario/${id}`, {
    headers: { Authorization: `Bearer ${token}` }
  });
  return res.data;
};

export const createEmployee = async (employee: any) => {
  const token = localStorage.getItem('token');
  // Ajuste para garantir que os nomes das propriedades estejam corretos
  const funcionario = {
    nome: employee.nome,
    sobrenome: employee.sobrenome,
    email: employee.email,
    documento: employee.documento,
    dataNascimento: employee.dataNascimento,
    role: employee.role,
    passwordHash: employee.passwordHash,
    nomeGerente: employee.nomeGerente,
    telefones: Array.isArray(employee.telefones) ? employee.telefones.map(t => ({ numero: t.numero })) : [],
  };
  const res = await axios.post(`${API_URL}/funcionario`, funcionario, {
    headers: { Authorization: `Bearer ${token}` }
  });
  return res.data;
};

export const updateEmployee = async (id: number, employee: any) => {
  const token = localStorage.getItem('token');
  const funcionario = {
    nome: employee.nome,
    sobrenome: employee.sobrenome,
    email: employee.email,
    documento: employee.documento,
    dataNascimento: employee.dataNascimento,
    role: employee.role,
    passwordHash: employee.passwordHash,
    nomeGerente: employee.nomeGerente,
    telefones: Array.isArray(employee.telefones) ? employee.telefones.map(t => ({ numero: t.numero })) : [],
  };
  await axios.put(`${API_URL}/funcionario/${id}`, funcionario, {
    headers: { Authorization: `Bearer ${token}` }
  });
};

export const deleteEmployee = async (id: number) => {
  const token = localStorage.getItem('token');
  await axios.delete(`${API_URL}/funcionario/${id}`, {
    headers: { Authorization: `Bearer ${token}` }
  });
}
