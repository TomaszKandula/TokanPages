const API_VER = 1;

/* BASE URL */

export const APP_FRONTEND = `http://localhost:3000`;
export const APP_BACKEND  = `http://localhost:5000`;

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

/* API | SUBSCRIBERS */

export const API_GET_SUBSCRIBERS   = `${APP_BACKEND}/api/v${API_VER}/subscribers/`;
export const API_GET_SUBSCRIBER    = `${APP_BACKEND}/api/v${API_VER}/subscribers/{id}`;
export const API_POST_SUBSCRIBER   = `${APP_BACKEND}/api/v${API_VER}/subscribers/`;
export const API_PATCH_SUBSCRIBER  = `${APP_BACKEND}/api/v${API_VER}/subscribers/`;
export const API_DELETE_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/{id}`;

/* STATIC CONTENT */

export const STORY_URL  = `${APP_FRONTEND}/static/mystory.html`;
export const TERMS_URL  = `${APP_FRONTEND}/static/terms.html`;
export const POLICY_URL = `${APP_FRONTEND}/static/policy.html`;

/* MESSAGE TEMPLATES */

export const MESSAGE_OUT_SUCCESS  = `<p>Congratulations!</p><p>The message has been sent successfully, we will try to respond as soon as possible.</p>`;
export const MESSAGE_OUT_WARN     = `<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To send an email all fields must be filled along with acceptance of Terms of Use and Privacy Policy.</span>`;
export const MESSAGE_OUT_ERROR    = `<p>The message couldn't be sent.</p><p>Error has been thrown: {ERROR}</p>`;
export const NEWSLETTER_SUCCESS   = `<p>Congratulations!</p><p>You have been added to the newsletter.</p>`;
export const NEWSLETTER_WARN      = `<span>The newsletter couldn't be setup, following warning(s) received:</span><ul>{LIST}</ul><span>Please make sure the email is valid and you have access to it.</span>`;
export const NEWSLETTER_ERROR     = `<p>The newsletter couldn't be setup.</p><p>Error has been thrown: {ERROR}</p>`;
export const SUBSCRIBER_DEL_ERROR = `<p>We could not remove your email from our newsletter list.</p><p>Error has been thrown: {ERROR}</p><p>Please contact us to resolve the issue.</p>`;
