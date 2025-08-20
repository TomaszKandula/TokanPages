import * as React from "react";
import Slider from "react-slick";
import { GET_NEWS_URL } from "../../../../Api/Paths";
import { LinkDto, NewsItemDto } from "../../../../Api/Models";
import { ViewProperties } from "../../../../Shared/Abstractions";
import { ReactChangeEvent, ReactKeyboardEvent } from "../../../../Shared/types";
import {
    CustomImage,
    Icon,
    ProgressBar,
    RedirectTo,
    RenderHtml,
    Skeleton,
    TextField,
    TextFieldWithPassword,
} from "../../../../Shared/Components";
import { UserSigninProps } from "../userSignin";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import "./userSigninView.css";

interface UserSigninViewProps extends ViewProperties, UserSigninProps {
    caption: string;
    button: string;
    link1: LinkDto;
    link2: LinkDto;
    security: NewsItemDto[];
    buttonHandler: () => void;
    progress: boolean;
    keyHandler: (event: ReactKeyboardEvent) => void;
    formHandler: (event: ReactChangeEvent) => void;
    email: string;
    password: string;
    labelEmail: string;
    labelPassword: string;
}

const ActiveButton = (props: UserSigninViewProps): React.ReactElement => (
    <button
        type="submit"
        onClick={props.buttonHandler}
        className="bulma-button bulma-is-link bulma-is-light bulma-is-fullwidth"
        disabled={props.progress}
    >
        {!props.progress ? props.button : <ProgressBar size={20} />}
    </button>
);

export const UserSigninView = (props: UserSigninViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container">
            <div className="bulma-columns mx-4 my-6">
                <div className="bulma-column bulma-is-half p-0">
                    <div className="bulma-card user-signin-view-margins">
                        <div className="bulma-card-content">
                            <div className="is-flex is-flex-direction-column is-align-items-center">
                                <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                                    <Icon name="AccountCircle" size={3.75} className="card-icon-colour" />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Text">
                                    <p className="is-size-3 has-text-black">{props.caption}</p>
                                </Skeleton>
                            </div>
                            <div className="my-5">
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                    <TextField
                                        required
                                        uuid="email"
                                        autoComplete="email"
                                        onKeyUp={props.keyHandler}
                                        onChange={props.formHandler}
                                        value={props.email}
                                        placeholder={props.labelEmail}
                                        isDisabled={props.progress}
                                        className="mb-5"
                                    />
                                </Skeleton>
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                    <TextFieldWithPassword
                                        uuid="password"
                                        value={props.password}
                                        placeholder={props.labelPassword}
                                        onKeyUp={props.keyHandler}
                                        onChange={props.formHandler}
                                        isDisabled={props.progress}
                                    />
                                </Skeleton>
                            </div>
                            <div className="mb-5">
                                <Skeleton isLoading={props.isLoading} mode="Rect">
                                    <ActiveButton {...props} />
                                </Skeleton>
                            </div>
                            <div className="is-flex is-flex-direction-row is-justify-content-space-between">
                                <div className="my-2">
                                    <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={30}>
                                        <RedirectTo path={props.link1?.href} name={props.link1?.text} />
                                    </Skeleton>
                                </div>
                                <div className="my-2">
                                    <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={30}>
                                        <RedirectTo path={props.link2?.href} name={props.link2?.text} />
                                    </Skeleton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div className="bulma-column is-flex is-align-self-center is-justify-content-center user-signin-view-margins">
                    <div className="bulma-card user-signin-view-card-news">
                        <Skeleton isLoading={props.isLoading} mode="Rect" width={350} height={300}>
                            <Slider
                                dots={false}
                                arrows={false}
                                fade={true}
                                infinite={true}
                                slidesToShow={1}
                                slidesToScroll={1}
                                autoplay={true}
                                autoplaySpeed={5500}
                                pauseOnHover={true}
                                waitForAnimate={false}
                            >
                                {props.security.map((value: NewsItemDto, index: number) => (
                                    <React.Fragment key={index}>
                                        <div className="bulma-card-image">
                                            <figure className="bulma-image">
                                                <CustomImage
                                                    base={GET_NEWS_URL}
                                                    source={value.image}
                                                    className="user-signin-view-card-image"
                                                    title="Security news image"
                                                    alt="Illustration for security news"
                                                />
                                            </figure>
                                        </div>
                                        <div className="bulma-card-content p-0 pt-3 pb-4">
                                            <div className="bulma-tags m-0 px-5 pb-3">
                                                {value.tags.map((value: string, index: number) => (
                                                    <span
                                                        className="bulma-tag bulma-is-warning bulma-is-light"
                                                        key={index}
                                                    >
                                                        {value}
                                                    </span>
                                                ))}
                                            </div>
                                            <RenderHtml
                                                value={value.title}
                                                tag="div"
                                                className="is-size-6 has-text-weight-semibold px-5 py-2"
                                            />
                                            <RenderHtml value={value.lead} tag="div" className="is-size-6 px-5 py-2" />
                                        </div>
                                    </React.Fragment>
                                ))}
                            </Slider>
                        </Skeleton>
                    </div>
                </div>
            </div>
        </div>
    </section>
);
