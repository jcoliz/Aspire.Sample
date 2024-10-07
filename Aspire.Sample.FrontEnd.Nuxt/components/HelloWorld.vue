<script setup lang="ts">

defineProps<{
  msg: string
}>()

</script>

<template>
  <div class="greetings" data-test-id="HelloWorld">
    <h1 class="green">{{ msg }}</h1>
    <p>
      This FrontEnd is build using <a href="https://vitejs.dev/" target="_blank" rel="noopener">Nuxt</a>.
    </p>
  </div>
</template>

<style scoped>
</style>
