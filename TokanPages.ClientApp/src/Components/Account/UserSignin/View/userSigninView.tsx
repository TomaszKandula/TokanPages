import * as React from "react";
import { GET_IMAGES_URL } from "../../../../Api";
import { NewsItemDto } from "../../../../Api/Models";
import { RenderSigninCardProps, RenderSlideProps, UserSigninViewProps } from "../Types";
import {
    Image,
    Icon,
    Link,
    Notification,
    ProgressBar,
    RenderHtml,
    Skeleton,
    TextField,
    TextFieldWithPassword,
    Slider,
} from "../../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";
import "./userSigninView.css";

const ButtonSignin = (props: UserSigninViewProps): React.ReactElement => (
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
        <div className="bulma-tags m-0 pb-3">
            {props.tags.map((value: string, _index: number) => (
                <span className="bulma-tag bulma-is-warning" key={uuidv4()}>
                    {value}
                </span>
            ))}
        </div>
    );

const RenderSlide = (props: RenderSlideProps): React.ReactElement => (
    <div className="m-4">
        <div className="bulma-card-image">
            <figure className="bulma-image">
                <Skeleton isLoading={props.isLoading ?? false} mode="Rect" height={256} disableMarginY>
                    <Image
                        base={GET_IMAGES_URL}
                        source={props.image}
                        className="user-signin-view-card-image"
                        title="Security news image"
                        alt="Illustration for security news"
                        loading="lazy"
                    />
                </Skeleton>
            </figure>
        </div>
        <div className="bulma-card-content p-0 pt-3">
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={100}>
                <RenderTags {...props} />
            </Skeleton>
            <hr className="m-0" />
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={75} className="my-4">
                <p className="is-size-7 pt-3">{props.date}</p>
            </Skeleton>
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={250}>
                <RenderHtml value={props.title} tag="h2" className="is-size-6 has-text-weight-semibold pt-1 pb-3" />
            </Skeleton>
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={48} width={350}>
                <RenderHtml value={props.lead} tag="p" className="is-size-6" />
            </Skeleton>
        </div>
    </div>
);

const RenderSigninCard = (props: RenderSigninCardProps): React.ReactElement => (
    <div className={`bulma-card ${props.className ?? ""}`}>
        <div className="bulma-card-content">
            <div>
                <div className="is-flex is-flex-direction-column is-align-items-center">
                    <Skeleton isLoading={props.isLoading} mode="Circle" width={72} height={72}>
                        <Icon name="AccountCircle" size={3.75} className="card-icon-colour" />
                    </Skeleton>
                    <Skeleton isLoading={props.isLoading} mode="Text">
                        <h2 className="is-size-3 has-text-black">{props.caption}</h2>
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
                        <ButtonSignin {...props} />
                    </Skeleton>
                </div>
            </div>
            <Skeleton isLoading={props.isLoading} mode="Rect" height={90}>
                <Notification text={props.consent} hasIcon />
            </Skeleton>
            <div className="is-flex is-flex-direction-row is-justify-content-space-between user-signin-view-bottom-container">
                <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={30}>
                    <Link to={props.link1?.href}>
                        <>{props.link1?.text}</>
                    </Link>
                </Skeleton>
                <Skeleton isLoading={props.isLoading} mode="Text" width={100} height={30}>
                    <Link to={props.link2?.href}>
                        <>{props.link2?.text}</>
                    </Link>
                </Skeleton>
            </div>
        </div>
    </div>
);

export const UserSigninView = (props: UserSigninViewProps): React.ReactElement => (
    <section className={props.className}>
        <div className="bulma-container pb-6">
            <div className="bulma-columns mx-4 my-6">
                <div className="bulma-column is-flex is-justify-content-center p-0">
                    <RenderSigninCard {...props} className="user-signin-view-card-signin" />
                </div>
                <div className="bulma-column is-flex is-justify-content-center p-0">
                    <Slider
                        isLoading={props.isLoading}
                        isLazyLoad={true}
                        isFading={false}
                        isInfinite={true}
                        isSwipeToSlide={true}
                        isNavigation={true}
                        autoplay={true}
                        autoplaySpeed={5500}
                        pauseOnHover={true}
                        className="user-signin-view-card-news is-flex is-flex-direction-column"
                    >
                        {props.isLoading ? (
                            <RenderSlide isLoading={props.isLoading} image="" tags={[""]} date="" title="" lead="" />
                        ) : (
                            props.security.map((value: NewsItemDto, _index: number) => (
                                <RenderSlide
                                    key={uuidv4()}
                                    image={value.image}
                                    tags={value.tags}
                                    date={value.date}
                                    title={value.title}
                                    lead={value.lead}
                                />
                            ))
                        )}
                    </Slider>
                </div>
            </div>
        </div>
    </section>
);
