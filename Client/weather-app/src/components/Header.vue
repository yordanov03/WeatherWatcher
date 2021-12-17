<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarSupportedContent"
      aria-controls="navbarSupportedContent"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item active">
          <a class="nav-link" href="#">
            Home
            <span class="sr-only">(current)</span>
          </a>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {
  name: "Header",
  props: {
    msg: String
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
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

.weather {
  display: flex;
  flex-flow: column wrap;
  box-shadow: 0px 1px 10px 0px #cfcfcf;
  overflow: hidden;
}

.weather .current {
  display: flex;
  flex-flow: row wrap;
  background-image: url("http://www.prepbootstrap.com/Content/images/shared/misc/london-view.png");
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
  background: url(https://www.amcharts.com/wp-content/themes/amcharts4/css/img/icons/weather/animated/cloudy-day-1.svg)
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

.weather .future .day:hover {
  color: #fff;
  background-color: #f68d2e;
}

.weather .future .day h3 {
  text-transform: uppercase;
}

.weather .future .day p {
  font-size: 28px;
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