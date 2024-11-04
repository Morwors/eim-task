<script lang="ts" setup>
  import { ref, computed } from 'vue'
  import { FwbButton, FwbModal } from 'flowbite-vue'
  import Spinner from '../shared/Spinner.vue'

  // Import the task store
  import { taskStore } from '@/store/task.store' // Adjust the path based on your project structure

  const isShowModal = ref(false)
  const loading = ref(false)

  // Form data bindings
  const name = ref('')
  const description = ref('')
  const errorMessage = ref('')

  // Validation for name field: at least 3 characters and alphanumeric
  const isNameValid = computed(() => {
    return name.value.length >= 3 && /^[a-zA-Z0-9\s]+$/.test(name.value);
  })

  // Functions to control modal visibility
  function closeModal() {
    isShowModal.value = false
  }
  function showModal() {
    isShowModal.value = true
  }

  // Handle form submission
  async function handleCreateTask() {
    if (!isNameValid.value) return // Prevent submission if validation fails
    loading.value = true

    try {
      const taskData = {
        title: name.value,
        description: description.value,
      }

      await taskStore.createTask(taskData)

      closeModal()
      name.value = ''
      description.value = ''
    } catch (error) {
      errorMessage.value = error.message
      console.error('Failed to create task:', error)
    } finally {
      loading.value = false
    }
  }

  defineExpose({
    closeModal,
    showModal
  })
</script>

<template>
  <fwb-modal v-if="isShowModal" @close="closeModal" size="md">
    <template #header>
      <h2>Create a new Task</h2>
    </template>
    <template #body>
      <form @submit.prevent="handleCreateTask" class="max-w-sm mx-auto">
        <div class="mb-5">
          <label for="name" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Task Name</label>
          <input type="text"
                 id="name"
                 v-model="name"
                 class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                 placeholder="Refactor Code"
                 required />
          <!-- Validation message for name -->
          <p v-if="!isNameValid" class="text-red-500 text-sm mt-1">
            Name must be at least 3 alphanumeric characters.
          </p>
        </div>
        <div class="mb-5">
          <label for="description" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Task description</label>
          <textarea id="description"
                    v-model="description"
                    rows="6"
                    placeholder="Complete JSTask code refactoring"
                    class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                    required></textarea>
        </div>
        <div v-if="errorMessage" class="mb-5">
          <span class="text-sm text-red-500">{{errorMessage}}</span>

        </div>

        <button type="submit"
                class="bg-orange-600 rounded-2xl p-2 text-white w-32 font-medium hover:shadow-inner items-center justify-center disabled:opacity-50"
                :disabled="loading || !isNameValid">
          <Spinner v-if="loading" class="h-6" :size="4"/>
          {{ loading ? '' : 'Create' }}
        </button>
      </form>
    </template>
  </fwb-modal>
</template>



