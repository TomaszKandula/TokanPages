import * as React from "react";
import Slider from "react-slick";
import { GET_IMAGES_URL } from "../../../../Api";
import { NewsItemDto } from "../../../../Api/Models";
import {
    CustomImage,
    Icon,
    Link,
    Notification,
    ProgressBar,
    RenderHtml,
    Skeleton,
    TextField,
    TextFieldWithPassword,
} from "../../../../Shared/Components";
import { v4 as uuidv4 } from "uuid";
import { RenderSigninCardProps, RenderSlideProps, RenderSliderProps, UserSigninViewProps } from "../Types";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
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
        <div className="bulma-tags m-0 px-5 pb-3">
            {props.tags.map((value: string, _index: number) => (
                <span className="bulma-tag bulma-is-warning" key={uuidv4()}>
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
                        base={GET_IMAGES_URL}
                        source={props.image}
                        className="user-signin-view-card-image"
                        title="Security news image"
                        alt="Illustration for security news"
                    />
                </Skeleton>
            </figure>
        </div>
        <div className="bulma-card-content p-0 pt-3">
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={100} className="mx-5">
                <RenderTags {...props} />
            </Skeleton>
            <hr className="m-0" />
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={75} className="mx-5 my-4">
                <p className="is-size-7 px-5 pt-3">{props.date}</p>
            </Skeleton>
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={250} className="mx-5">
                <RenderHtml value={props.title} tag="h2" className="is-size-6 has-text-weight-semibold px-5 py-3" />
            </Skeleton>
            <Skeleton isLoading={props.isLoading ?? false} mode="Text" height={24} width={350} className="mx-5">
                <RenderHtml value={props.lead} tag="p" className="is-size-6 px-5" />
            </Skeleton>
        </div>
    </>
);

const RenderSlider = (props: RenderSliderProps): React.ReactElement => {
    const [selection, setSelection] = React.useState(0);

    const handleChange = React.useCallback((_current: number, next: number) => {
        setSelection(next);
    }, []);

    return (
        <div className={`bulma-card ${props.className ?? ""}`}>
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
                beforeChange={handleChange}
            >
                {props.isLoading ? (
                    <RenderSlide isLoading={props.isLoading} image="" tags={[]} date="" title="" lead="" />
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
            <div className="is-flex is-justify-content-center is-gap-1.5 p-5 user-signin-view-bottom-container">
                {props.security.map((_value: NewsItemDto, index: number) => (
                    <Icon
                        key={uuidv4()}
                        name="Circle"
                        size={0.6}
                        className={selection === index ? "has-text-grey-dark" : "has-text-grey-light"}
                    />
                ))}
            </div>
        </div>
    );
};

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
                    <RenderSlider {...props} className="user-signin-view-card-news is-flex is-flex-direction-column" />
                </div>
            </div>
        </div>
    </section>
);
