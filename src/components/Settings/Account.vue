<template>
  <div class="row q-px-md q-pt-sm">
    <div class="col-12">
      <q-input label="Username" filled dense v-model="username" />
    </div>
    <div class="col-12 q-mt-sm">
      <q-input label="Password" filled dense v-model="password" type="password" />
    </div>
  </div>
</template>

<script>
export default {
  name: "Account",
  data() {
    return {
      password: null,
      username: null
    };
  }
};
</script>
