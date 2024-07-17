<script setup lang="ts">
import { ref } from "vue";
import { useToast } from "primevue/usetoast";

const toast = useToast();
const increaseCount = () => {
    count.value++;

    if (count.value % 3 === 0) {
        toast.add({ severity: "success", summary: "Aspire.Sample", detail: "Thank you for using Aspire.Sample", life: 3000 });

    }
};

const count = ref(0);

</script>
<template>
<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: {{ count }}</p>

<PrimeButton @click="increaseCount" label="Click me"></PrimeButton>


</template>