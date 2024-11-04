<script setup>
  import { onMounted } from 'vue'
  import Spinner from './shared/Spinner.vue'
  import TaskItem from './TaskItem.vue'
  import { taskStore } from '../store/task.store'

  // on mount fetch tasks
  onMounted(() => {
    taskStore.getTasks()
  })
  // on input search change fetch tasks
  const search = (e) => {
    taskStore.getTasks(e.target.value)
  }

</script>

<template>
  <Spinner v-if="taskStore.loading" class="absolute top-1/2 left-1/2 transform -translate-x-1/2 -translate-y-1/2" />
  <div class="flex flex-col m-4 gap-4 w-full">
    <form class="sm:w-full md:w-1/2">
      <label for="search" class="mb-2 text-sm font-medium text-gray-900 sr-only dark:text-white">Search</label>
      <div class="relative">
        <div class="absolute z-10 inset-y-0 start-0 flex items-center ps-3 pointer-events-none">
          <svg class="w-4 h-4 text-gray-500 dark:text-gray-400" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 20 20">
            <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m19 19-4-4m0-7A7 7 0 1 1 1 8a7 7 0 0 1 14 0Z" />
          </svg>
        </div>
        <input @input="search" type="search" id="search" class="block w-full p-4 ps-10 text-sm text-gray-900 border border-gray-300 rounded-2xl bg-white focus:ring-orange-600 focus:border-orange-600 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-orange-600 dark:focus:border-orange-600 shadow-inner" placeholder="Search" required />
      </div>
    </form>
    <div class="w-full gap-4 relative grid lg:grid-cols-3 md:grid-cols-2 grid-cols-1 xl:grid-cols-4 h-fit">
      <TaskItem v-for="task in taskStore.tasks" :key="task.id" :task="task" />
    </div>
  </div>

</template>

