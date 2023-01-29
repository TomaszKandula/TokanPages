import * as React from "react";

interface ProgressStyle 
{
    width: number;
    height: number;
    bgcolor: string;
    duration: number;
}

const ProgressStyle = (props: ProgressStyle): React.CSSProperties => 
({
    margin: 0,
    padding: 0,
    position: "fixed",
    top: 0,
    zIndex: 99,
    backgroundColor: `${props.bgcolor}`,
    height: `${props.height}px`,
    width: `${props.width}%`,
    transitionProperty: "width",
    transitionDuration: `${props.duration}s`,
    transitionTimingFunction: "ease-out"
});

interface ProgressOnScroll 
{
    height: number;
    bgcolor: string;
    duration: number;
}

export const ProgressOnScroll = (props: ProgressOnScroll) => 
{
    const [width, setWidth] = React.useState(0);

    const Scrolling = () => 
    {
        const winScroll: number = document.body.scrollTop || document.documentElement.scrollTop;
        const height: number = document.documentElement.scrollHeight - document.documentElement.clientHeight;
        const scrolled: number = (winScroll / height) * 100;
      
        if (height > 0) 
        {
            setWidth(scrolled);
        } 
        else 
        {
            setWidth(0);
        }
    }

    React.useEffect(() => 
    {
        window.addEventListener("scroll", Scrolling);
        return () => window.removeEventListener("scroll", Scrolling);
    }, []);

    return (
        <div 
            style={ProgressStyle({ width: width, ...props })}>
        </div>
    );
}
