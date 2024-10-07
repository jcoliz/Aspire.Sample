<script setup lang="ts">
import { ref } from "vue";

const increaseCount = () => {
    count.value++;
};

const count = ref(0);

</script>

<template>
    <h1>Counter</h1>
    <p role="status">Current count: {{ count }}</p>
    <PrimeButton @click="increaseCount" label="Click me"></PrimeButton>
</template>