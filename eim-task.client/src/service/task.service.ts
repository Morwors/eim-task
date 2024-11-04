import axios from "axios";

export async function get(filter) {
  return await axios.get(`https://localhost:7270/api/Tasks?filter=${filter}`)
}

export async function create(task) {
  return await axios.post(`https://localhost:7270/api/Tasks`, task)
}

export async function update(task) {
  return await axios.put(`https://localhost:7270/api/Tasks/${task.id}`, task)
}

export async function remove(taskId) {
  return await axios.delete(`https://localhost:7270/api/Tasks/${taskId}`)
}
