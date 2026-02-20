Can look for object that hold scripts by filtering in Hierarchy for "__" (double underscore)

* FAN
the fan has 2 particle effects (particle system (PS)) - stands for plus and minus
ParticleSystemEmitterListener sits on the fan's PS - they "send" when the particles hit other object
the message is "heard" by the SimManager

Once - all PS on the card are hit by he fans (only if they\re the opposite), it's gone. new one will be created after few seconds

THIS IS **NOT** ACCURATE
