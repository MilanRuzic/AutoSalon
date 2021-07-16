<template>
  <div class="mainContainer">
    <div class="divHeader"><h2>PRETRAGA</h2></div>
    <div class="tableRow">
      <div class="proizvodjac">
        <select-list name="PROIZVODJAC">
          <select-option
            @find="findModels"
            :options="manufactors"
          ></select-option>
        </select-list>
      </div>
      <div class="proizvodjac">
        <select-list name="MODEL:">
          <select-option
            @find="findVehicles"
            :disabled="enableModels"
            :options="models"
          ></select-option>
        </select-list>
      </div>
      <div class="proizvodjac">
        <label><b>CIJENA:</b></label
        ><br />
        <div>
          <label>OD:</label
          ><input type="range" min="0" max="200000" v-model="priceFrom" />
        </div>
        <label>{{ priceFrom }}</label>
        <div>
          <label>DO:</label
          ><input type="range" min="0" max="200000" v-model="priceTo" />
        </div>
        <label>{{ priceTo }}</label>
      </div>
    </div>
    <div class="tableRow">
      <div class="proizvodjac">
        <select-list name="VRSTA GORIVA">
          <select-option
            @find="findFuelTypes"
            :options="fuelTypes"
          ></select-option>
        </select-list>
      </div>

      <div class="proizvodjac">
        <label><b>KILOMETRAZA:</b></label
        ><br />
        <div>
          <label>OD:</label
          ><input type="range" min="0" max="2000000" v-model="kilometersFrom" />
        </div>
        <label>{{ kilometersFrom }}</label>
        <div>
          <label>DO:</label
          ><input type="range" min="0" max="2000000" v-model="kilometersTo" />
        </div>
        <label>{{ kilometersTo }}</label>
      </div>
      <div class="proizvodjac">
        <label><b>SNAGA MOTORA:</b></label
        ><br />
        <div>
          <label>OD:</label
          ><input type="range" min="0" max="300" v-model="motorPowerFrom" />
        </div>
        <label>{{ motorPowerFrom }}</label>
        <div>
          <label>DO:</label
          ><input type="range" min="0" max="300" v-model="motorPowerTo" />
        </div>
        <label>{{ motorPowerTo }}</label>
      </div>
    </div>
    <div class="priceAll">
      <input type="checkbox" id="checkbox" v-model="checked" /><label>
        ISKLJUCI PRODATA VOZILA</label
      >
    </div>

    <button @click="filterVehicles">PRETRAZI</button>
  </div>
</template>
<script>
import axios from "axios";
import SelectList from "../select/SelectList.vue";
import SelectOption from "../select/SelectOption.vue";

export default {
  components: { SelectList, SelectOption },
  data() {
    return {
      manufactors: [],
      models: [],
      fuelTypes: [],
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
    };
  },
  computed: {
    enableModels() {
      if (this.selected > 0) {
        return false;
      } else {
        return true;
      }
    },
  },

  methods: {
    findFuelTypes(selected) {
      this.fuelTypeId = selected;
    },
    findModels(selected) {
      this.selected = selected;
      this.models = [];
      let models = [];
      if (this.selected > 0) {
        models = this.manufactors.find((t) => t.id === this.selected).models;
        for (var model of models) {
          this.models.push({ id: model.modelid, name: model.nazivmodela });
        }
      }
    },
    findVehicles(selected) {
      this.selectedModelId = selected;
    },
    getManufactors() {
      axios
        .get("https://localhost:44353/api/Proizvodjacs")
        .then((response) => {
          //this.manufactors = response.data;
          const manufactors = [];
          for (var data of response.data) {
            manufactors.push({
              id: data.proizvodjacid,
              name: data.nazivproizvodjaca,
              models: data.models,
            });
          }
          this.manufactors = manufactors;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getFuelType() {
      axios
        .get("https://localhost:44353/api/VrstaGorivas")
        .then((response) => {
          //this.manufactors = response.data;
          const fuelType = [];
          for (var data of response.data) {
            fuelType.push({
              id: data.vrstagorivaid,
              name: data.nazivvrstegoriva,
            });
          }
          this.fuelTypes = fuelType;
        })
        .catch((error) => {
          console.log(error);
        });
    },
    filterVehicles() {
      this.$router.push({
        name: "filter-vehicle",
        query: {
          proizvodjac: this.selected,
          model: this.selectedModelId,
          cenaod: this.priceFrom,
          cenado: this.priceTo,
          vrstagoriva: this.fuelTypeId,
          kilometrazaod: this.kilometersFrom,
          kilometrazado: this.kilometersTo,
          snagamotoraod: this.motorPowerFrom,
          snagamotorado: this.motorPowerTo,
          prodata: this.checked,
        },
      });
    },
  },
  mounted() {
    this.getManufactors();
    this.getFuelType();
  },
};
</script>
<style>
.divHeader {
  width: 100%;
  text-align: center;
}
button {
  margin-top: 1rem;
}
.mainContainer {
  display: table;
  width: 100%;
  text-align: center;
}
.tableRow {
  display: table-row;
}
.proizvodjac {
  display: table-cell;
  width: 30%;
  height: 7rem;
  margin: 0 auto;
}
.priceAll {
  display: flex;
  font-weight: bold;
  margin-top: 1rem;
  text-align: left;
  color: grey;
}
.price {
  margin-top: 1rem;

  width: 70%;
}
.priceLabel {
  display: inline-block;
}
</style>
