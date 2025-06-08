import numpy as np
import aubio
from pydub import AudioSegment
import json

# Funkcija za RMS vrednost na določenem segmentu
def get_rms(samples):
    return np.sqrt(np.mean(samples**2))

beats_with_strength = []

src = aubio.source("trimmed.wav", 0, hop_s)
tempo_o = aubio.tempo("default", win_s, hop_s, samplerate)

while True:
    samples, read = src()
    if tempo_o(samples):
        beat_time = tempo_o.get_last_s()
        # Izračun RMS iz trenutno prebranih vzorcev
        strength = get_rms(samples)
        beats_with_strength.append((beat_time, strength))
    if read < hop_s:
        break

# Shrani kot JSON s močjo beata
with open("beatmap_strength.json", "w") as f:
    json.dump(beats_with_strength, f, indent=2)
