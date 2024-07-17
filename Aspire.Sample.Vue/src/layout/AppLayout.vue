<script setup lang="ts">
import AppMenu from "./AppMenu.vue";
import { RouterView } from "vue-router"
</script>
<template>
    <div class="page" data-test-id="AppLayout">
        <div class="sidebar">
            <AppMenu />
        </div>

        <main>
            <div class="top-row px-4">
                <a href="https://vuejs.org/" target="_blank">About</a>
            </div>

            <article class="content px-4">
                <router-view/>
            </article>
        </main>
        <PrimeToast/>
    </div>
</template>
