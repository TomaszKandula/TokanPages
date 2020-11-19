const API_VER = 1;

/* API | GENERAL */

export const APP_FRONTEND = `http://localhost:3000`;
export const APP_BACKEND  = `http://localhost:5000`;
export const APP_STORAGE  = `https://maindbstorage.blob.core.windows.net/tokanpages`;

/* API | STATIC CONTENT */

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

/* MESSAGE TEMPLATES */

export const MESSAGE_OUT_WARN    = `<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To send an email all fields must be filled along with acceptance of Terms of Use and Privacy Policy.</span>`;
export const MESSAGE_OUT_SUCCESS = `The message has been sent successfully!`;
export const MESSAGE_OUT_ERROR   = `The message couldn't be sent. Error has been thrown: {ERROR}.`;
