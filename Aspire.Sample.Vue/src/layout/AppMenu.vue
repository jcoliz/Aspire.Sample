<script setup lang="ts">
import { RouterLink } from "vue-router"
</script>
<template>
    <div class="top-row ps-3 navbar navbar-dark">
        <div class="container-fluid">
            <a class="navbar-brand" href="">Aspire.Sample</a>
        </div>
    </div>

    <input type="checkbox" title="Navigation menu" class="navbar-toggler" />

    <div class="nav-scrollable" onclick="document.querySelector('.navbar-toggler').click()">
        <nav class="flex-column">
            <div class="nav-item px-3">
                <RouterLink class="nav-link" to="/">
                    <span class="bi bi-house-door-fill" aria-hidden="true"></span> Home
                </RouterLink>
            </div>

            <div class="nav-item px-3">
                <RouterLink class="nav-link" to="/counter">
                    <span class="bi bi-plus-square-fill" aria-hidden="true"></span> Counter
                </RouterLink>
            </div>

            <div class="nav-item px-3">
                <RouterLink class="nav-link" to="/weather">
                    <span class="bi bi-list-nested" aria-hidden="true"></span> Weather
                </RouterLink>
            </div>
        </nav>
    </div>
</template>
