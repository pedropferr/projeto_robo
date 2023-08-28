<template>
  <v-app-bar flat>
    <v-app-bar-title class="align-center text-center justify-center text-h4">
      <v-col>
        <v-icon icon="mdi-robot" />

        <span class="font-weight-bold mt-3"> R.O.B.O </span>

        <v-btn class="mr-auto" icon="mdi-invert-colors" @click="toggleTheme()">
        </v-btn>
      </v-col>
    </v-app-bar-title>
  </v-app-bar>
</template>

<script setup>
import { useTheme } from "vuetify";

const theme = useTheme();

function toggleTheme() {
  theme.global.name.value = theme.global.current.value.dark ? "light" : "dark";
}
</script>

