import { createApp } from "vue";
import App from "./App.vue";
import BaseCard from "./components/UI/BaseCard.vue";
import { createStore } from "vuex";
import { createRouter, createWebHistory } from "vue-router";
import axios from "axios";
import VehicleList from "./components/Vehicles/VehicleList.vue";
import ShowVehicle from "./components/Vehicles/ShowVehicle.vue";
import createPersistedState from "vuex-persistedstate";

const store = createStore({
  state() {
    return {
      vehicles: [],
      vehicle: {},
      isLoading: false,
      filter: false,
      defaultFilter: {
        selectedModelId: "0",
        priceFrom: 0,
        priceTo: 200000,
        fuelTypeId: "0",
        selected: "0",
        kilometersFrom: 0,
        kilometersTo: 2000000,
        motorPowerFrom: 0,
        motorPowerTo: 300,
        checked: false,
      },
      user: null,
      badLoginData: false,
    };
  },
  plugins: [createPersistedState()],
  mutations: {
    badFilter(state, payload) {
      state.filter = payload.value;
    },
    getVehicles(state, payload) {
      state.vehicles = payload.value;
    },
    changeLoading(state, payload) {
      state.isLoading = payload.value;
    },
    selectVehicle(state, payload) {
      state.vehicle = payload.value[0];
    },
    sortVehicles(state, payload) {
      if (payload.value == 1) {
        state.vehicles.sort((a, b) => {
          return b.cena - a.cena;
        });
      } else if (payload.value == 2) {
        state.vehicles.sort((a, b) => {
          return a.cena - b.cena;
        });
      }
    },
    getUser(state, payload) {
      state.user = payload.value;
    },
    loginData(state, payload) {
      state.badLoginData = payload.value;
    },
    logOut(state) {
      state.user = null;
    },
  },
  actions: {
    deleteVehicle(context, payload) {
      console.log(payload.value);
      axios
        .delete("https://localhost:44353/api/Voziloes/" + payload.value)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    selectVehicle(context, payload) {
      console.log(payload);
      context.commit("changeLoading", { value: true });
      axios
        .get("https://localhost:44353/api/Voziloes/" + payload.value)
        .then((response) => {
          context.commit("changeLoading", { value: false });
          context.commit("selectVehicle", { value: response.data });
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getVehicles(context) {
      context.commit("changeLoading", { value: true });
      axios
        .get("https://localhost:44353/api/Voziloes")
        .then((response) => {
          context.commit("getVehicles", { value: response.data });
          context.commit("changeLoading", { value: false });
        })
        .catch((error) => {
          console.log(error);
        });
    },
    sortVehicles(context, payload) {
      context.commit("sortVehicles", { value: payload.value });
    },
    filterVehicles(context, payload) {
      context.commit("changeLoading", { value: true });
      axios
        .get("https://localhost:44353/api/Voziloes/filter", {
          params: {
            proizvodjacId: payload.proizvodjacid,
            modelId: payload.modelid,
            cenaOd: payload.cenaod,
            cenaDo: payload.cenado,
            vrstagorivaId: payload.vrstaGorivaid,
            kilometrazaOd: payload.kilometrazaod,
            kilometrazaDo: payload.kilometrazado,
            snagaMotoraOd: payload.snagaMotoraod,
            snagaMotoraDo: payload.snagaMotorado,
            prodata: payload.prodata,
          },
        })
        .then((response) => {
          if (response.data.length > 0) {
            context.commit("getVehicles", { value: response.data });
            context.commit("badFilter", { value: false });
          } else {
            context.state.vehicles = [];
            context.commit("badFilter", { value: true });
          }
          context.commit("changeLoading", { value: false });
        });
    },
    getUser(context, payload) {
      axios
        .get("https://localhost:44353/api/Korisniks/" + payload.username, {
          params: { username: payload.username, password: payload.password },
        })
        .then((response) => {
          console.log(response.data);
          context.commit("getUser", {
            value: response.data,
          });

          context.commit("loginData", { value: false });
        })
        .catch((error) => {
          console.log(error);
          context.commit("loginData", { value: true });
        });
    },
    logOut(context) {
      context.commit("logOut");
    },
  },
  getters: {
    style(state) {
      return state.vehicle.slikes[0].nazivslike;
    },
    getSelectedVehicle(state) {
      return state.vehicle;
    },
    badFilters(state) {
      return state.filter;
    },
    showVehicles(state) {
      return state.vehicles;
    },
    loading(state) {
      return state.isLoading;
    },
    getDefaultFilter(state) {
      return state.defaultFilter;
    },
    getUser(state) {
      return state.user;
    },
    getBadLoginData(state) {
      return state.badLoginData;
    },
  },
});

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      component: VehicleList,
    },
    {
      path: "/Vehicles",
      component: VehicleList,
    },
    { name: "show-vehicle", path: "/showVehicle", component: ShowVehicle },
    {
      name: "filter-vehicle",
      path: "/filterVehicles",
      component: VehicleList,
    },
  ],
});

const app = createApp(App);

app.use(store);
app.component("base-card", BaseCard);
app.use(router);
app.mount("#app");
