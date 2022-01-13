// Copyright (c) SquadroCorp
// This file is part of Squadrosu!.
// Squadrosu! is licensed under the GPL v3. See LICENSE.md for details.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;

namespace Squadrosu.Game.UI;

public class SquadrosuHueSelector : HSVColourPicker.HueSelector
{
    private const float corner_radius = 6;

    public SquadrosuHueSelector()
    {
        SliderBar.CornerRadius = corner_radius;
        SliderBar.Masking = true;
    }

    protected override Drawable CreateSliderNub() => new SliderNub(this);

    private class SliderNub : CompositeDrawable
    {
        private readonly Bindable<float> hue;

        public SliderNub(SquadrosuHueSelector selector)
        {
            hue = selector.Hue.GetBoundCopy();
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            var pointerTexture = textures.Get(@"hue_pointer_half");
            pointerTexture.ScaleAdjust = 1f;

            InternalChild = new Container
            {
                Padding = new MarginPadding(-10),
                RelativeSizeAxes = Axes.Y,
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                Children = new Drawable[]
                {
                    new Sprite
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.TopCentre,
                        TextureRelativeSizeAxes = Axes.Both,
                        Texture = pointerTexture,
                    },
                    new Sprite
                    {
                        Origin = Anchor.Centre,
                        Anchor = Anchor.BottomCentre,
                        Texture = pointerTexture,
                        Rotation = 180,
                    },
                },
            };
        }

        private void hueChangedEventHandler(ValueChangedEvent<float> hue)
        {
            Colour = Colour4.FromHSV(hue.NewValue, 1, 1);
        }

        protected override void LoadComplete()
        {
            hue.BindValueChanged(hueChangedEventHandler, true);
        }
    }
}
