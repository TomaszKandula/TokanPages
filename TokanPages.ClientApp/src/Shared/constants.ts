import { IsRunningOnIOS } from "./Services/IsRunningOnIOS";

export const DELAY_3_SECONDS: number = 3000;
export const DELAY_30_SECONDS: number = 30000;
export const SET_INTERVAL_DELAY: number = IsRunningOnIOS() ? DELAY_3_SECONDS : DELAY_30_SECONDS;

export const LIKES_LIMIT_FOR_ANONYM: number = 25;
export const LIKES_LIMIT_FOR_USER: number = 50;
export const WORDS_PER_MINUTE: number = 265;
export const INTERNAL_SUBJECT_TEXT = "Incoming Business Inquiry";
export const INTERNAL_MESSAGE_TEXT = "Please check the internal payload for more details.";
export const SELECTED_LANGUAGE: string = "SELECTED_LANGUAGE";
export const RECEIVED_ERROR_MESSAGE: string = "RECEIVED_ERROR_MESSAGE";
export const NO_ERRORS: string = "NO_ERRORS";
export const USER_DATA: string = "USER_DATA";
export const PDF_WORKER_URL = "https://cdn.jsdelivr.net/npm/pdfjs-dist@3.11.174/build/pdf.worker.min.js";
