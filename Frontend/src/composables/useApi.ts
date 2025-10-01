import { readonly } from 'vue';
import api from '@/lib/api';
import * as helpers from '@/lib/apiHelpers';
import type { AxiosInstance } from 'axios';


export function useApi() {
    return {
        api: readonly(api) as AxiosInstance,
        getData: helpers.getData,
        postData: helpers.postData,
        putData: helpers.putData,
        deleteData: helpers.deleteData,
    };
}