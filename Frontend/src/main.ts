import { createApp } from 'vue'
import '@/style.css'
import App from '@/App.vue'
import { router } from '@/router'
import { setupApiInterceptor } from './lib/api';

const app = createApp(App);
app.use(router);
router.isReady().then(() => {
   setupApiInterceptor(router);
   app.mount('#app');
});