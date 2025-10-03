import api from '@/lib/api/api';
import type { AxiosRequestConfig } from 'axios';

export async function getData<T = any>(url: string, config?: AxiosRequestConfig): Promise<T> {
    const res = await api.get<T>(url, config);
    return res.data;
}

export async function postData<T = any, U = any>(url: string, payload?: U, config?: AxiosRequestConfig): Promise<T> {
const res = await api.post<T>(url, payload, config);
return res.data;
}

export async function putData<T = any, U = any>(url: string, payload?: U, config?: AxiosRequestConfig): Promise<T> {
    const res = await api.put<T>(url, payload, config);
    return res.data;
}

export async function deleteData<T = any>(url: string, config?: AxiosRequestConfig): Promise<T> {
    const res = await api.delete<T>(url, config);
    return res.data;
}