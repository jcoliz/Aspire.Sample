<script setup lang="ts">
import { ref } from "vue";
import { useToast } from "primevue/usetoast";

const toast = useToast();

defineProps<{
  msg: string
}>()

const increaseCount = () => {
    count.value++;

    if (count.value === 3) {
        toast.add({ severity: "success", summary: "PrimeVue", detail: "Welcome to PrimeVue + Create Vue", life: 3000 });

        count.value = 0;
    }
};

const count = ref(0);


</script>

<template>
  <div class="greetings">
    <PrimeToast/>
    <h1 class="green">{{ msg }}</h1>
    <h3>
      You’ve successfully created a project with
      <a href="https://vitejs.dev/" target="_blank" rel="noopener">Vite</a> +
      <a href="https://vuejs.org/" target="_blank" rel="noopener">Vue 3</a>. What's next?
    </h3>

    <PrimeButton @click="increaseCount" label="Count!"></PrimeButton>
    <h5 class="green">{{ count }}</h5>
  </div>
</template>

<style scoped>
h1 {
  font-weight: 500;
  font-size: 2.6rem;
  position: relative;
  top: -10px;
}

h3 {
  font-size: 1.2rem;
}

.greetings h1,
.greetings h3 {
  text-align: center;
}

@media (min-width: 1024px) {
  .greetings h1,
  .greetings h3 {
    text-align: left;
  }
}
</style>
