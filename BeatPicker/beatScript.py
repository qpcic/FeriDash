from pydub import AudioSegment
import aubio
import json
import numpy as np

# Trim pesem na 40 sekund
audio = AudioSegment.from_file("glasba.mp3")
audio = audio[:40000]  # 40 sekund
audio.export("trimmed.wav", format="wav")

# Beat detekcija
win_s = 512
hop_s = 256
src = aubio.source("trimmed.wav", 0, hop_s)
samplerate = src.samplerate
tempo_o = aubio.tempo("default", win_s, hop_s, samplerate)

beats = []
while True:
    samples, read = src()
    if tempo_o(samples):
        beats.append(tempo_o.get_last_s())
    if read < hop_s:
        break

# Shrani JSON
with open("beatmap.json", "w") as f:
    json.dump(beats, f, indent=2)

print("Shranjeni beati:", len(beats))

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

beats_with_strength_serializable = [(float(t), float(s)) for t, s in beats_with_strength]

# Shrani kot JSON s močjo beata
with open("beatmap_strength.json", "w") as f:
    json.dump(beats_with_strength_serializable, f, indent=2)

