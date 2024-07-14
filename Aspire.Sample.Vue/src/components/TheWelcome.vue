<script setup lang="ts">
import { ref, onMounted } from "vue"
import * as api from "../apiclient/apiclient"

/**
 * Forecast data to display
 */

const forecasts = ref<api.IWeatherForecast[]>()

/**
 * Whether we are loading data from the server presently
 */
 const isLoading = ref(false)

 /**
 * Client for communicating with server
 */
const client = new api.ApiClient("/api")

/**
 * Get items from the server
 */
 async function getData() {
  forecasts.value = undefined
  isLoading.value = true

  client.getWeatherforecast()
    .then((result) => {
      forecasts.value = result
    })
    .finally(() => {
      isLoading.value = false
    })
}

/**
 * When mounted, get the view data from server
 */
 onMounted(() => {
  getData()
})

</script>

<template>
  <table>
    <thead>
      <tr>
        <th>Date</th>
        <th>Temp. (C)</th>
        <th>Temp. (F)</th>
        <th>Summary</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="forecast in forecasts">
        <td>{{ forecast.date?.toLocaleDateString() }}</td>
        <td>{{ forecast.temperatureC }}</td>
        <td>{{ forecast.temperatureF }}</td>
        <td>{{ forecast.summary }}</td>
      </tr>
    </tbody>
  </table>
</template>
