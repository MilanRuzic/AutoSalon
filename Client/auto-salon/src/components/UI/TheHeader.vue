<template>
  <div id="header">
    <slot></slot>
  </div>
</template>
<style scoped>
html,
body {
  margin: 0;
  padding: 0;
}

#header {
  border-top: 7px solid #505559;

  width: 100%;
  background: #eee;
  height: 4rem;
  padding: 0;
  font: 12px arial, sans-serif;
}
</style>
