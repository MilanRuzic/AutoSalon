<template>
  <select class="select" @change="find" v-model="selected">
    <option value="0">--</option>
    <option v-for="option in options" :key="option.id" :value="option.id">{{
      option.name
    }}</option>
  </select>
</template>
<script>
export default {
  props: ["options"],
  data() {
    return {
      selected: "0",
    };
  },
  methods: {
    find() {
      this.$emit("find", this.selected);
    },
  },
};
</script>
<style scoped>
.select {
  background-color: #fbfbfb;
  border: 1px solid #ddd;
  border-radius: 2px;
  font-family: Arial;
  font-size: 12px;
  line-height: 30px;
  margin-top: 0px;
  padding: 6px 4px 5px 2px;
  width: 100%;
  box-sizing: border-box;
}
</style>
