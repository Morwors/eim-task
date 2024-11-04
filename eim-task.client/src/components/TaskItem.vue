<script setup>
  import { ref, onMounted } from 'vue'
  import IconCanlendar from './icons/IconCalendar.vue'
  import IconPen from './icons/IconPen.vue'
  import IconTrash from './icons/IconTrash.vue'
  import DeleteItem from './modals/DeleteItem.vue'
  import { taskStore } from '../store/task.store'
  import moment from 'moment'
  defineProps(['task'])



</script>

<template>
  <div class="rounded-xl drop-shadow-xl p-4 flex flex-col gap-4 bg-white justify-between">
    <div class="flex flex-col gap-4">
      <div class="inline-flex justify-between items-center">
        <h3 class="font-medium text-lg">{{task.title}}</h3>
        <button @click="taskStore.selectTask(task)" class="hover:text-red-600">
          <IconTrash />
        </button>
      </div>
      <p>{{task.description}}</p>
    </div>

    <div class="inline-flex justify-between items-center">
      <button @click="taskStore.selectEditTask(task)" class="hover:text-orange-600">
        <IconPen />
      </button>
      <div class="p-2 border border-gray-400 rounded-2xl w-fit self-end inline-flex items-center gap-2">
        <IconCanlendar /> {{moment(task.createdAt).format('DD/MM/YYYY')}}
      </div>
    </div>


  </div>
</template>
