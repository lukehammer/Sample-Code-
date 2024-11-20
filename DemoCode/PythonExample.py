scipy.signal.butter(N=1, Wn=cutoff, btype="high", fs=sample_rate, output="sos")



burst_len = arry.shape[-1]
    nyquist = sample_rate / 2
    cutoff = 0.01 * nyquist * max(np.log((burst_len / 8192) ** (-3 / 2)), 1)
    filt = scipy.signal.butter(N=1, Wn=cutoff, btype="high", fs=sample_rate, output="sos")
    return cast(NDArray[double], scipy.signal.sosfilt(filt, arry))







Parameters
N=1:

Specifies the filter's order. A first-order filter (N=1) will have a gradual roll-off compared to higher-order filters.
Wn=cutoff:

Defines the critical frequency (or cutoff frequency) of the filter, in Hz. The high-pass filter will allow frequencies higher than this value to pass through while attenuating lower frequencies.
btype="high":

Specifies the filter type as high-pass.
fs=sample_rate:

The sampling frequency of the signal, in Hz. This is necessary to interpret the cutoff frequency in terms of normalized digital frequency.
output="sos":

Specifies that the filter coefficients should be returned in second-order sections (SOS) format. This format is numerically more stable and preferred for digital filtering operations compared to the traditional polynomial (ba) representation.