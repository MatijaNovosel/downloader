<template>
  <div class="row q-px-sm">
    <div class="col-12">
      <q-toggle size="sm" label="Dark mode" v-model="settings.darkMode"></q-toggle>
    </div>
  </div>
</template>

<script>
export default {
  name: "Appearance",
  data() {
    return {
      settings: {
        darkMode: true
      }
    };
  },
  watch: {
    settings: {
      deep: true,
      immediate: false,
      handler(oldVal, newVal) {
        this.$q.dark.toggle();
        if (oldVal.darkMode != newVal.darkMode) {
          this.$q.dark.toggle();
        }
      }
    }
  }
};
</script>
