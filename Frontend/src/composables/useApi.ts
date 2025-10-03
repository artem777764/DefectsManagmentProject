import { readonly } from 'vue';
import api from '@/lib/api/api';
import * as helpers from '@/lib/api/apiHelpers';
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