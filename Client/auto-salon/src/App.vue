<template>
  <div>
    <the-header>
      <form v-if="!userExists" id="search" @submit.prevent="submitUser">
        <label
          >Email <input type="text" name="s-user" id="s-user" v-model="username"
        /></label>

        <label
          >Password<input
            type="text"
            name="s-pass"
            id="s-pass"
            v-model="password"
        /></label>
        <input type="submit" class="submit" value="Submit" />
      </form>
      <button class="logOut" v-else @click="logOut">Log out</button>
    </the-header>
    <error-message v-if="$store.getters.getBadLoginData"
      ><p>
        Uneli ste pogresne podatke za logovanje, pokusajte ponovo!
      </p></error-message
    >
    <router-view></router-view>
  </div>
</template>
<script>
import TheHeader from "./components/UI/TheHeader.vue";
import ErrorMessage from "./components/UI/ErrorMessage.vue";
export default {
  components: {
    TheHeader,
    ErrorMessage,
  },
  data() {
    return {
      username: "",
      password: "",
    };
  },
  computed: {
    userExists() {
      return this.$store.getters.getUser;
    },
  },
  methods: {
    submitUser() {
      if (this.username.length > 0)
        this.$store.dispatch("getUser", {
          username: this.username,
          password: this.password,
        });
    },
    logOut() {
      this.username = "";
      this.password = "";
      this.$store.dispatch("logOut");
    },
  },
};
</script>
<style>
body {
  padding: 0;
  margin: 0;
}
form {
  float: right;
  margin-right: 30px;
  height: 40px;
  padding-top: 8px;
}

form label {
  display: inline-block;
  margin: 0 2px;
}

form #s-user,
form #s-pass {
  display: block;
  width: 250px;
  border: 1px solid #eee;
  padding: 3px 0 3px 0;
  margin-bottom: -1px;
}

form .submit {
  height: 23px;
  vertical-align: bottom;
  background: #4e4d4e;
  color: #fff;
  border: 1px solid #ddd;
  -moz-border-radius: 5px;
  border-radius: 5px;
}
.logOut {
  position: absolute;
  right: 100px;
  width: 10%;
  height: 2rem;
  background-color: rgb(75, 75, 241);
  color: white;
  border: solid 1px;
  border-radius: 15px;
}
</style>
