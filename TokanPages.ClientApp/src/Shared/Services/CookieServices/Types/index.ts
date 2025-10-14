interface BaseProps {
    cookieName: string;
}

export interface EraseCookieProps extends BaseProps {
}

export interface GetCookieProps  extends BaseProps {
}

export interface SetCookieProps extends BaseProps {
    value: string;
    days: number;
    sameSite: string;
    secure: boolean;
    exact?: string;
}
