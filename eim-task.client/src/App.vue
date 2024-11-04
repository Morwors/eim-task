<script setup>
  import { ref } from 'vue'
  import Header from './components/Header.vue'
  import Spinner from './components/shared/Spinner.vue'
  import TaskContainer from './components/TaskContainer.vue'
  import DeleteItem from './components/modals/DeleteItem.vue'
  import AddItem from './components/modals/AddItem.vue'
  import EditItem from './components/modals/EditItem.vue'

  const addItemModal = ref(null);

  function testCall() {
    addItemModal.value.showModal();
  }
</script>

<template class="min-h-screen">
  <div class="min-h-screen flex flex-col">
    <header class="w-full h-14 flex flex-row p-2 px-7 items-center justify-between drop-shadow">
      <h1 class="font-medium text-2xl">Task<span class="text-orange-600">JS</span></h1>
      <button @click="testCall()" class="bg-orange-600 rounded-2xl p-2 text-white w-32 font-medium hover:shadow-inner">Add Task</button>
    </header>
    <main class="flex-1 flex bg-gray-100">
      <TaskContainer />
    </main>
  </div>
  <DeleteItem/>
  <EditItem/>
  <AddItem ref="addItemModal"/>


</template>
