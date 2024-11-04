import { reactive } from 'vue'
import { get as getTaskService, create as createTaskService, update as updateTaskService, remove as removeTaskService } from '@/service/task.service'

export const taskStore = reactive({
  tasks: [],
  selectedTask: null,
  loading: true,
  page: 1,
  limit: 10,
  total: 0,
  modalOpen: false,
  addModalOpen: false,
  selectTask(task) {
    if (!task) {
      this.modalOpen = false
    } else {
      this.modalOpen = true
    }
    this.selectedTask = task;
  },
  selectEditTask(task) {
    if (!task) {
      this.addModalOpen = false
    } else {
      this.addModalOpen = true
    }
    console.log("editing task", task)
    this.selectedTask = task;
    },
  async getTasks(filter = "") {
    this.loading = true
    const response = await getTaskService(filter)
    console.log(response)
    this.tasks = response.data
    this.loading = false
  },
  async createTask(task) {
    this.loading = true
    const response = await createTaskService(task)
    console.log(response)
    this.getTasks()
  },
  async updateTask(task) {
    this.loading = true
    const response = await updateTaskService(task)
    console.log(response)
    this.getTasks()
  },
  async removeTask(taskId) {
      this.loading = true
      const response = await removeTaskService(taskId)
      console.log(response)
      this.getTasks()
  }

})
