const API_VER = 1;

/* BASE URL */

export const APP_FRONTEND = `http://localhost:3000`;
export const APP_BACKEND  = `http://localhost:5000`;
export const APP_STORAGE  = `https://maindbstorage.blob.core.windows.net/tokanpages`;

/* STATIC CONTENT */

export const STORY_URL   = `${APP_FRONTEND}/static/mystory.html`;
export const TERMS_URL   = `${APP_FRONTEND}/static/terms.html`;
export const POLICY_URL  = `${APP_FRONTEND}/static/policy.html`;

/* API | ARTICLES */

export const API_GET_ARTICLES   = `${APP_BACKEND}/api/v${API_VER}/articles/`;
export const API_GET_ARTICLE    = `${APP_BACKEND}/api/v${API_VER}/articles/{id}`;
export const API_POST_ARTICLE   = `${APP_BACKEND}/api/v${API_VER}/articles/`;
export const API_PATCH_ARTICLE  = `${APP_BACKEND}/api/v${API_VER}/articles/`;
export const API_DELETE_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/{id}`;

/* API | MAILER */

export const API_GET_INSPECTION  = `${APP_BACKEND}/api/v${API_VER}/mailer/inspection/`;
export const API_POST_MESSAGE    = `${APP_BACKEND}/api/v${API_VER}/mailer/message/`;
export const API_POST_NEWSLETTER = `${APP_BACKEND}/api/v${API_VER}/mailer/newsletter/`;

/* OTHER ASSETS */

export const IMG_LOGO   = `${APP_STORAGE}/icons/main_logo.svg`;
export const IMG_TOMEK  = `${APP_STORAGE}/images/tomek_bergen.jpg`;
export const IMG_ADAMA  = `${APP_STORAGE}/images/section_testimonials/adama.jpg`;
export const IMG_JOANNA = `${APP_STORAGE}/images/section_testimonials/joanna.jpg`;
export const IMG_SCOTT  = `${APP_STORAGE}/images/section_testimonials/scott.jpg`;
export const IMG_FEAT1  = `${APP_STORAGE}/images/section_featured/article1.jpg`;
export const IMG_FEAT2  = `${APP_STORAGE}/images/section_featured/article2.jpg`;
export const IMG_FEAT3  = `${APP_STORAGE}/images/section_featured/article3.jpg`;
export const IMG_ART1   = `${APP_STORAGE}/images/section_articles/image1.jpg`;
export const IMG_ART2   = `${APP_STORAGE}/images/section_articles/image2.jpg`;
export const IMG_ART3   = `${APP_STORAGE}/images/section_articles/image3.jpg`;
export const IMG_ART4   = `${APP_STORAGE}/images/section_articles/image4.jpg`;

/* MESSAGE TEMPLATES */

export const MESSAGE_OUT_WARN    = `<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To send an email all fields must be filled along with acceptance of Terms of Use and Privacy Policy.</span>`;
export const MESSAGE_OUT_SUCCESS = `The message has been sent successfully!`;
export const MESSAGE_OUT_ERROR   = `The message couldn't be sent. Error has been thrown: {ERROR}.`;
