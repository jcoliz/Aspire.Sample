<script setup lang="ts">

defineProps<{
  msg: string
}>()

</script>

<template>
  <div class="greetings" data-test-id="HelloWorld">
    <PrimeToast/>
    <h1 class="green">{{ msg }}</h1>
    <p>
      You’ve successfully created a project with
      <a href="https://vitejs.dev/" target="_blank" rel="noopener">Vite</a> +
      <a href="https://vuejs.org/" target="_blank" rel="noopener">Vue 3</a>. What's next?
    </p>
  </div>
</template>

<style scoped>
</style>
