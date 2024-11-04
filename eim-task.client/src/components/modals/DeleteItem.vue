<script setup lang="ts">
  import { taskStore } from '../../store/task.store'
  import Spinner from '../shared/Spinner.vue'

  import { ref } from 'vue'
  import { FwbButton, FwbModal } from 'flowbite-vue'

  const isShowModal = ref(false)
  const loading = ref(false)
  const errorMessage = ref("")
  function closeModal() {
    taskStore.selectTask(null)
  }
  function showModal() {
  }

  async function handleDeleteTask() {
    try {
      loading.value = true
      if (!taskStore.selectedTask) {
        throw new Error('Task not found')
      }
      await taskStore.removeTask(taskStore.selectedTask.id)
      closeModal()
    } catch (error) {
      errorMessage.value = error.message
      console.error('Failed to delete task:', error)
    } finally {
      loading.value = false
    }
  }
  
</script>

<template>
  <fwb-modal v-if="taskStore.modalOpen" @close="closeModal">
    <template #body>
      <div class="relative text-center bg-white rounded-lg dark:bg-gray-800">
        <svg class="text-gray-400 dark:text-gray-500 w-11 h-11 mb-3.5 mx-auto" aria-hidden="true" fill="currentColor" viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg"><path fill-rule="evenodd" d="M9 2a1 1 0 00-.894.553L7.382 4H4a1 1 0 000 2v10a2 2 0 002 2h8a2 2 0 002-2V6a1 1 0 100-2h-3.382l-.724-1.447A1 1 0 0011 2H9zM7 8a1 1 0 012 0v6a1 1 0 11-2 0V8zm5-1a1 1 0 00-1 1v6a1 1 0 102 0V8a1 1 0 00-1-1z" clip-rule="evenodd"></path></svg>
        <p class="mb-4 text-gray-500 dark:text-gray-300">Are you sure you want to delete this item?</p>
        <div class="flex justify-center items-center space-x-4">
          <button :disabled="loading" data-modal-toggle="deleteModal" type="button" class="py-2 px-3 text-sm font-medium text-gray-500 bg-white rounded-lg border border-gray-200 hover:bg-gray-100 focus:ring-4 focus:outline-none focus:ring-primary-300 hover:text-gray-900 focus:z-10 dark:bg-gray-700 dark:text-gray-300 dark:border-gray-500 dark:hover:text-white dark:hover:bg-gray-600 dark:focus:ring-gray-600">
            No, cancel
          </button>
          <button @click="handleDeleteTask()" :disabled="loading" type="submit" class="py-2 px-3 text-sm font-medium text-center text-white bg-red-600 rounded-lg hover:bg-red-700 focus:ring-4 focus:outline-none focus:ring-red-300 dark:bg-red-500 dark:hover:bg-red-600 dark:focus:ring-red-900 w-28 disabled:opacity-50">
            <Spinner v-if="loading" class="h-5" :size="4" />
            {{ loading ? '' : 'Yes im sure' }}

          </button>
        </div>
      </div>
    </template>
    
  </fwb-modal>
</template>
