const HomePage = { template: "<home></home>" };
const Login = { template: "<login></login>" };
const Registration = { template: "<registration></registration>" }
const HomeUser = { template: "<homeUser></homeUser>" }
const ProfileUser = { template: "<profileUser></profileUser>" }
const ListUsers = { template: "<listUsers></listUsers>" }
const Proba = { template: "<proba></proba>" }
const NewAppointment = { template: "<newAppointment></newAppointment>" }
const Appointments = { template: "<appointments></appointments>" }
const Review = { template: "<review></review>" }
const Users = { template: "<listUsers></listUsers>" }
const Reviews = { template: "<reviews></reviews>" }

const routes = [
  { path: "/", component: HomePage },
  { path: "/login", component: Login },
  { path: "/registration", component: Registration },
  { path: "/homeUser", component: HomeUser },
  { path: "/profileUser", component: ProfileUser },
  { path: "/proba", component: Proba },
  { path: "/newAppointment", component: NewAppointment },
  { path: "/appointments", component: Appointments },
  { path: "/review", component: Review },
  { path: "/patients", component: Users },
  { path: "/reviews", component: Reviews },
];

const router = new VueRouter({
  routes,
});

axios.defaults.headers.common['Authorization'] = 'Bearer ' + localStorage.getItem('username');

const app = new Vue({
  router,
}).$mount("#app");

