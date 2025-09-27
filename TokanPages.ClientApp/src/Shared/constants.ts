import { IsRunningOnIOS } from "./Services/IsRunningOnIOS";

export const APP_BAR_HEIGHT_DESKTOP = 64;
export const APP_BAR_HEIGHT_NON_DESKTOP_TOP = 48;
export const APP_BAR_HEIGHT_NON_DESKTOP_BOTOM = APP_BAR_HEIGHT_DESKTOP;
export const APP_BAR_HEIGHT_NON_DESKTOP = APP_BAR_HEIGHT_DESKTOP + APP_BAR_HEIGHT_NON_DESKTOP_TOP;

export const DELAY_3_SECONDS = 3000;
export const DELAY_30_SECONDS = 30000;
export const SET_INTERVAL_DELAY = IsRunningOnIOS() ? DELAY_3_SECONDS : DELAY_30_SECONDS;

export const LIKES_LIMIT_FOR_ANONYM = 25;
export const LIKES_LIMIT_FOR_USER = 50;
export const WORDS_PER_MINUTE = 265;
export const ARTICLES_PAGE_SIZE = 4;

export const INTERNAL_SUBJECT_TEXT = "Incoming Business Inquiry";
export const INTERNAL_MESSAGE_TEXT = "Please check the internal payload for more details.";
export const RECEIVED_ERROR_MESSAGE = "RECEIVED_ERROR_MESSAGE";
export const NO_ERRORS = "NO_ERRORS";
export const USER_DATA = "USER_DATA";
export const DEFAULT_USER_IMAGE = "avatar-default-288.webp";

export const PDF_JS_MIN_URL = "pdf.min.js";
export const PDF_WORKER_URL = "pdf.worker.min.js";
export const PRERENDER_PATH_PREFIX = "/snapshot";
