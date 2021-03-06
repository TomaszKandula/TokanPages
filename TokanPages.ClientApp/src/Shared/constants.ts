const API_VER = process.env.REACT_APP_API_VER;

/* BASE URL */

export const APP_FRONTEND = process.env.REACT_APP_FRONTEND;
export const APP_BACKEND = process.env.REACT_APP_BACKEND;
export const APP_STORAGE = process.env.REACT_APP_STORAGE;

/* API | ARTICLES */

export const API_QUERY_GET_ARTICLES = `${APP_BACKEND}/api/v${API_VER}/articles/GetAllArticles/`;
export const API_QUERY_GET_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/GetArticle/{id}/`;
export const API_COMMAND_ADD_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/AddArticle/`;
export const API_COMMAND_UPDATE_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/UpdateArticle/`;
export const API_COMMAND_REMOVE_ARTICLE = `${APP_BACKEND}/api/v${API_VER}/articles/RemoveArticle/`;

/* API | USERS */

export const API_QUERY_GET_USERS = `${APP_BACKEND}/api/v${API_VER}/users/GetAllUsers/`;
export const API_QUERY_GET_USER = `${APP_BACKEND}/api/v${API_VER}/users/GetUser/{id}/`;
export const API_COMMAND_ADD_USER = `${APP_BACKEND}/api/v${API_VER}/users/AddUser/`;
export const API_COMMAND_UPDATE_USER = `${APP_BACKEND}/api/v${API_VER}/users/UpdateUser/`;
export const API_COMMAND_REMOVE_USER = `${APP_BACKEND}/api/v${API_VER}/users/RemoveUser/`;

/* API | SUBSCRIBERS */

export const API_QUERY_GET_SUBSCRIBERS = `${APP_BACKEND}/api/v${API_VER}/subscribers/GetAllSubscribers/`;
export const API_QUERY_GET_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/GetSubscriber/{id}/`;
export const API_COMMAND_ADD_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/AddSubscriber/`;
export const API_COMMAND_UPDATE_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/UpdateSubscriber/`;
export const API_COMMAND_REMOVE_SUBSCRIBER = `${APP_BACKEND}/api/v${API_VER}/subscribers/RemoveSubscriber/`;

/* API | MAILER */

export const API_COMMAND_VERIFY_EMAIL = `${APP_BACKEND}/api/v${API_VER}/mailer/VerifyEmailAddress/`;
export const API_COMMAND_SEND_MESSAGE = `${APP_BACKEND}/api/v${API_VER}/mailer/SendMessage/`;
export const API_COMMAND_SEND_NEWSLETTER = `${APP_BACKEND}/api/v${API_VER}/mailer/SendNewsletter/`;

/* COMPONENTS CONTENT */

export const GET_NAVIGATION_CONTENT = `${APP_STORAGE}/content/components/navigation.json`;
export const GET_HEADER_CONTENT = `${APP_STORAGE}/content/components/header.json`;
export const GET_FOOTER_CONTENT = `${APP_STORAGE}/content/components/footer.json`;
export const GET_ARTICLE_FEAT_CONTENT = `${APP_STORAGE}/content/components/articleFeat.json`;
export const GET_CONTACT_FORM_CONTENT = `${APP_STORAGE}/content/components/contactForm.json`;
export const GET_COOKIES_PROMPT_CONTENT = `${APP_STORAGE}/content/components/cookiesPrompt.json`;
export const GET_FEATURED_CONTENT = `${APP_STORAGE}/content/components/featured.json`;
export const GET_FEATURES_CONTENT = `${APP_STORAGE}/content/components/features.json`;
export const GET_NEWSLETTER_CONTENT = `${APP_STORAGE}/content/components/newsletter.json`;
export const GET_RESET_FORM_CONTENT = `${APP_STORAGE}/content/components/resetForm.json`;
export const GET_SIGNIN_FORM_CONTENT = `${APP_STORAGE}/content/components/signinForm.json`;
export const GET_SIGNUP_FORM_CONTENT = `${APP_STORAGE}/content/components/signupForm.json`;
export const GET_TESTIMONIALS_CONTENT = `${APP_STORAGE}/content/components/testimonials.json`;
export const GET_UNSUBSCRIBE_CONTENT = `${APP_STORAGE}/content/components/unsubscribe.json`;
export const GET_UPDATE_SUBSCRIBER_CONTENT = `${APP_STORAGE}/content/components/updateSubscriber.json`;
export const GET_WRONG_PAGE_PROMPT_CONTENT = `${APP_STORAGE}/content/components/wrongPagePrompt.json`;

/* URLS */

export const STORY_URL = `${APP_STORAGE}/content/mystory.json`;
export const TERMS_URL = `${APP_STORAGE}/content/terms.json`;
export const POLICY_URL = `${APP_STORAGE}/content/policy.json`;
export const ARTICLE_URL = `${APP_STORAGE}/content/articles/{ID}/text.json`;
export const IMAGE_URL = `${APP_STORAGE}/content/articles/{ID}/image.jpg`;

/* PATHS */

export const ARTICLE_PATH = `/articles/?id={ID}`;
export const IMAGES_PATH = `${APP_STORAGE}/images/`;
export const TESTIMONIALS_PATH = `${APP_STORAGE}/images/section_testimonials/`;
export const ICONS_PATH = `${APP_STORAGE}/icons/`;
export const AVATARS_PATH = `${APP_STORAGE}/content/avatars/`;

/* SVG ICONS */

export const MAIN_ICON = `${ICONS_PATH}main_logo.svg`;
export const MEDIUM_ICON = `${ICONS_PATH}medium_icon.svg`;

/* MESSAGES AND TEMPLATES */

export const MESSAGE_OUT_SUCCESS: string = "<p>Congratulations!</p><p>The message has been sent successfully, we will try to respond as soon as possible.</p>";
export const MESSAGE_OUT_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>To send an email all fields must be filled along with acceptance of Terms of Use and Privacy Policy.</span>";
export const MESSAGE_OUT_ERROR: string = "<p>Ouch!</p><p>The message could not be sent.</p><p>{ERROR}.</p>";

export const NEWSLETTER_SUCCESS: string = "<p>Congratulations!</p><p>You have been added to the newsletter.</p>";
export const NEWSLETTER_WARNING: string = "<span>The newsletter could not be setup, following warning(s) received:</span><ul>{LIST}</ul><span>Please make sure the email is valid and you have access to it.</span>";
export const NEWSLETTER_ERROR: string = "<p>Ouch!</p><p>The newsletter could not be setup.</p><p>{ERROR}.</p>";

export const UPDATE_ARTICLE_SUCCESS: string = "<p>Success!</p><p>Article has been updated successfully</p>";
export const UPDATE_ARTICLE_WARNING: string = "<span>We have received following warning(s):</span><ul>{LIST}</ul><span>Please check if all required fields are filled correctly.</span>";
export const UPDATE_ARTICLE_ERROR: string = "<p>Ouch!</p><p>The article could not be updated.</p><p>{ERROR}.</p>";

export const SUBSCRIBER_DEL_ERROR: string = "<p>We could not remove your email from our newsletter list.</p><p{ERROR}.</p><p>Please contact us to resolve the issue.</p>";

export const UNEXPECTED_STATUS: string = "Received unexpected status code: {STATUS_CODE}. Please contact IT Support";
export const UNEXPECTED_ERROR: string = "Unexpected error occured";
export const VALIDATION_ERRORS: string = "Validation errors have been found";

/* OTHER */

export const LIKES_LIMIT_FOR_ANONYM: number = 25;
export const LIKES_LIMIT_FOR_USER: number = 50;
export const LIKES_HINT_FOR_ANONYM: string = "You may give up to {LEFT_LIKES} thumbs up!";
export const LIKES_HINT_FOR_USER: string = "You may give up to {LEFT_LIKES} thumbs up!";
export const MAX_LIKES_REACHED: string = "You've reached max thumbs up :)";
export const WORDS_PER_MINUTE: number = 265;
export const RECEIVED_ERROR_MESSAGE: string = "RECEIVED_ERROR_MESSAGE";
export const NO_ERRORS: string = "NO_ERRORS";

/* FORMS */

export const CONTACT_FORM: string = "Contact Form";
export const NEWSLETTER: string = "Newsletter";
export const UPDATE_SUBSCRIBER: string = "Update Subscriber";
export const REMOVE_SUBSCRIBER: string = "Remove Subscriber";
