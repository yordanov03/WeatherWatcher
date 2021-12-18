<template>
<div>
    <div class="row">
        <div class="col-md-12">
          <form style="margin:5px;">
            <div class="form-row">
              <div class="col-12 col-md-9 mb-2 mb-md-0">
                <input
                  type="text"
                  v-model="inputValue"
                  class="search"
                  placeholder="Enter a city or German postcode..."
                />
              </div>
              <div class="search-button-container">
                <button
                  type="button"
                  class="search-button"
                  v-on:click="searchWeather"
                >
                  Search
                </button>
              </div>

               <div
                v-if="showError"
                class="error-message"
            >
                Invalid name or zipcode
            </div>

            </div>
          </form>
        </div>
      </div>
        <div class="weather-container">
            <div
                v-for="(weather, index) in weatherForecast"
                :key="weather.date"
            >
                <div class="weather">
                    <div class="current">
                    <div class="info">
                        <div>&nbsp;</div>
                        <div class="city">
                        <small>
                            <small>CITY:</small>
                        </small>
                        {{ weather.city }}
                        </div>
                        <div class="temp">
                        <small>
                            <small>TEMP:</small>
                        </small>
                        {{weather.temperature}}&deg;
                        <small>C</small>
                        </div>
                        <div class="wind">
                        <small>
                            <small>WIND:</small>
                        </small>
                        {{weather.windSpeed}} km/h
                        </div>
                        <div class="humidity">
                        <small>
                            <small>HUMIDITY:</small>
                        </small>
                        {{weather.humidity}}%
                        </div>
                        <div>&nbsp;</div>
                    </div>
                    <div class="icon">
                        <div :class=weather.weatherDescription></div>
                    </div>
                    </div>
                    <div class="future">
                    <div class="day" :class="{ 'current-weather': index === 0 }">
                        <h4 class="date">{{weather.date}}</h4>
                    </div>
                    </div>
                </div>
            </div>
        </div>
            <p class="weather-history-label">Weather History</p>
            <div class="weatherr-history-container">
                    <div v-for="(weather, index) in weatherHistory" :key="weather.date" class="flex">
                        <div v-for="(w) in weather" :key="w.date">
                            <div class="weather">
                                <div class="current">
                                <div class="info">
                                    <div>&nbsp;</div>
                                    <div class="city">
                                    <small>
                                        <small>CITY:</small>
                                    </small>
                                    {{ w.city }}
                                    </div>
                                    <div class="temp">
                                    <small>
                                        <small>TEMP:</small>
                                    </small>
                                    {{w.temperature}}&deg;
                                    <small>C</small>
                                    </div>
                                    <div class="wind">
                                    <small>
                                        <small>WIND:</small>
                                    </small>
                                    {{w.windSpeed}} km/h
                                    </div>
                                    <div class="humidity">
                                    <small>
                                        <small>HUMIDITY:</small>
                                    </small>
                                    {{w.humidity}}%
                                    </div>
                                    <div>&nbsp;</div>
                                </div>
                                <div class="icon">
                                    <!-- <div class="sunny"></div> -->
                                     <div :class=w.weatherDescription></div>
                                </div>
                                <div class="future">
                                <div class="day" :class="{ 'current-weather': index === 0 }">
                                    <h4 class="date">{{w.date}}</h4>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
            </div>
            </div>     
</div>
 
</template>

<script>
import axios from "axios";

export default {
    name: 'Weather',
    
    data () {
        return {
            weatherForecast: [],
            inputValue: 'Bonn',
            weatherHistory: [],
            showError: false,
            overcastCloudy: 'snowy'
        }
    },

    computed: {
        axiosParams() {
            const params = new URLSearchParams();
            if (typeof(this.inputValue) === 'string') {
                params.append("city", this.inputValue)
            } else if (typeof(this.inputValue) === 'number' && this.inputValue.toString().length == 5){
                params.append("zipCode", this.inputValue);
            } else {
                this.invalidMessage()
            }
            return params;
        }
  },

  created() {
    this.fetch();
  },

  methods: {
    fetch() {
      axios
        .get("https://localhost:5001/WeatherForecast", {
          params: this.axiosParams
        })
        .then(response => {
          this.weatherForecast = response.data;
          this.weatherHistory.push(this.weatherForecast)
          this.addToLocalStorage('weather-history', JSON.stringify(this.weatherHistory))
        })
        .catch(e => {
        this.invalidMessage(e);
        });
    },

    searchWeather() {
      this.fetch();
    },

    addToLocalStorage (nameOfElement, inputValue) {
      localStorage.setItem(nameOfElement, inputValue)
    },

    invalidMessage () {
        this.showError = true;
        setTimeout(() => {
            this.showError = false;
        }, 5000);
    }
  },
  determineAnimation(description){
    if(description == 'overcast cloudy'){
      this.overcastCloudy = 'cloudy'
    }
  }
}
</script>

<style scoped>
.weather-container {
  display: flex;
  justify-content: center;
  align-items: center
}
.search {

 border: 1px solid grey;
 border-radius: 5px;
  height: 1.3em;
  width:20%;
  padding: 2px 10px 2px 10px;
  outline: 0;
  background-color: #f5f5f5;


}
.search-button-container {
  margin-top: 0.4em;
  margin-bottom: 2em;
}
.search-button {
 border-radius: 5px;
  border: 0px solid black;
  background: rgb(218,233,243);
  height: 2.3em;
}
.current-weather {
  color: #fff;
  background-color: #f68d2e;
}
.date {
  font-size: 1.2rem;
  text-align: center;
  padding-top: 5px;
}

.weather-history-label {
    display: inline-block;
    margin: 2em 0 1.3em;
    text-decoration: underline;
    flex-wrap: wrap;
}
.error-message {
    /* color: red; */
  color: #D8000C;
	background-color: #FFBABA;
  padding: 30px !important;
  border-radius: 45 !important;
  position: relative; 
  display: inline-block !important;
  box-shadow: 1px 1px 1px #aaaaaa;
  margin-top: 10px;
  background-image: url('https://i.imgur.com/GnyDvKN.png');
  background-repeat: no-repeat, repeat;
}
.weatherr-history-container {
    display: flex;
    flex-wrap: wrap;
}
.weather {
  display: flex;
  flex-flow: column wrap;
  box-shadow: 0px 1px 10px 0px #cfcfcf;
  overflow: hidden;
  background-image: url("https://cdn5.vectorstock.com/i/1000x1000/48/04/city-with-two-story-cartoon-house-vector-22694804.jpg");

  background-repeat: no-repeat, repeat;
  margin-right: 1em;
  border-radius: 10px;
}

.weather .current {
  display: flex;
  flex-flow: row wrap;
  background-repeat: repeat-x;
  color: white;
  text-shadow: 1px 1px #f68d2e;
}

.weather .current .info {
  display: flex;
  flex-flow: column wrap;
  justify-content: space-around;
  flex-grow: 2;
}

.weather .current .info .city {
  font-size: 26px;
}

.weather .current .icon {
  margin: 0;
  width: 80px;
  height: 80px; 
  -webkit-box-flex: 1;
  -ms-flex-positive: 1; 
  flex-grow: 1; 
}

.weather .current .icon .sunny{
  margin: 0;
  width: 80px;
  height: 80px; 
  -webkit-box-flex: 1;
  -ms-flex-positive: 1; 
  flex-grow: 1;
  background: url(https://www.amcharts.com/wp-content/themes/amcharts4/css/img/icons/weather/animated/day.svg)
    50% 50% / contain no-repeat;
}

.weather .current .icon .cloudy{
  margin: 0;
  width: 80px;
  height: 80px; 
  -webkit-box-flex: 1;
  -ms-flex-positive: 1; 
  flex-grow: 1;
  background: url(https://www.amcharts.com/wp-content/themes/amcharts4/css/img/icons/weather/animated/cloudy-day-1.svg)
    50% 50% / contain no-repeat;
}

.weather .current .icon .rainy{
  margin: 0;
  width: 80px;
  height: 80px; 
  -webkit-box-flex: 1;
  -ms-flex-positive: 1; 
  flex-grow: 1;
  background: url(https://www.amcharts.com/wp-content/themes/amcharts4/css/img/icons/weather/animated/rainy-7.svg)
    50% 50% / contain no-repeat;
}

.weather .current .icon .snowy{
  margin: 0;
  width: 80px;
  height: 80px; 
  -webkit-box-flex: 1;
  -ms-flex-positive: 1; 
  flex-grow: 1;
  background: url(https://www.amcharts.com/wp-content/themes/amcharts4/css/img/icons/weather/animated/snowy-6.svg)
    50% 50% / contain no-repeat;
}

.weather .future {
  display: flex;
  flex-flow: row nowrap;
}

.weather .future .day {
  flex-grow: 1;
  text-align: center;
  cursor: pointer;
}

.weather .future .day {
  color: #fff;
  background-color: #f68d2e;
}

.weather .future .day h3 {
  text-transform: uppercase;
}

.weather .future .day p {
  font-size: 28px;
}

.flex {
    display: flex;
    margin-bottom: 1em;
}
.city,
.wind,
.humidity {
  margin-right: 0.5em;
  padding-left: 1.3em;
}
/* footer */
footer {
  background: #222;
  color: #aaa;
  padding-top: 10px;
}

footer a {
  color: #aaa;
}

footer a:hover {
  color: #fff;
}

footer h3 {
  color: #0894d1;
  letter-spacing: 1px;
  margin: 30px 0 20px;
}

footer .three-column {
  overflow: hidden;
}

footer .three-column li {
  width: 33.3333%;
  float: left;
  padding: 5px 0;
}

footer .socila-list {
  overflow: hidden;
  margin: 20px 0 10px;
}

footer .socila-list li {
  float: left;
  margin-right: 3px;
  opacity: 0.7;
  overflow: hidden;
  border-radius: 50%;
  transition: all 0.3s ease-in-out;
}

footer .socila-list li:hover {
  opacity: 1;
}

footer .img-thumbnail {
  background: rgba(0, 0, 0, 0.2);
  border: 1px solid #444;
  margin-bottom: 5px;
}

footer .copyright {
  padding: 15px 0;
  background: #333;
  margin-top: 20px;
  font-size: 15px;
}

footer .copyright span {
  color: #0894d1;
}

/* weather */
.icon.bigger {
  width: 57px;
  height: 57px;
}
.icon {
  width: 34px;
  height: 34px;
  display: inline-block;
  vertical-align: middle;
  margin: -3px 12px 0 0;
  background-size: contain;
  background-position: center center;
  background-repeat: no-repeat;
  text-indent: -9999px;
}
</style>