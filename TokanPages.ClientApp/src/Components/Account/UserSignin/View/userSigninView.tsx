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
import { v4 as uuidv4 } from "uuid";
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

interface RenderSlideProps {
    image: string;
    tags: string[];
    title: string;
    lead: string;
    isLoading?: boolean;
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

const RenderTags = (props: RenderSlideProps): React.ReactElement | null =>
    props.tags.length < 1 ? null : (
        <div className="bulma-tags m-0 px-5 pb-3">
            {props.tags.map((value: string, _index: number) => (
                <span className="bulma-tag bulma-is-warning bulma-is-light" key={uuidv4()}>
                    {value}
                </span>
            ))}
        </div>
    );

const RenderSlide = (props: RenderSlideProps): React.ReactElement => (
    <>
        <div className="bulma-card-image">
            <figure className="bulma-image">
                <Skeleton isLoading={props.isLoading ?? false} mode="Rect" height={150} disableMarginY>
                    <CustomImage
                        base={GET_NEWS_URL}
                        source={props.image}
                        className="user-signin-view-card-image"
                        title="Security news image"
                        alt="Illustration for security news"
                    />
                </Skeleton>
            </figure>
        </div>
        <div className="bulma-card-content p-0 pt-3 pb-4">
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={100} className="mx-5">
                <RenderTags {...props} />
            </Skeleton>
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={250} className="mx-5">
                <RenderHtml value={props.title} tag="div" className="is-size-6 has-text-weight-semibold px-5 py-2" />
            </Skeleton>
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={350} className="mx-5">
                <RenderHtml value={props.lead} tag="div" className="is-size-6 px-5 py-2" />
            </Skeleton>
        </div>
    </>
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
                            {props.isLoading ? (
                                <RenderSlide
                                    isLoading={props.isLoading}
                                    image=""
                                    tags={[]}
                                    title=""
                                    lead=""
                                />
                            ) : (
                                props.security.map((value: NewsItemDto, _index: number) => (
                                    <RenderSlide
                                        key={uuidv4()}
                                        image={value.image}
                                        tags={value.tags}
                                        title={value.title}
                                        lead={value.lead}
                                    />
                                ))
                            )}
                        </Slider>
                    </div>
                </div>
            </div>
        </div>
    </section>
);
